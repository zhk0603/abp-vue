using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.VueTemplate.MenuManagement.Authorization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    [Authorize]
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid, MenuRequestDto,
            CreateOrUpdateMenuDto, CreateOrUpdateMenuDto>,
        IMenuAppService
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly IMenuGrantRepository _menuGrantRepository;
        private readonly IMenuManager _menuManager;
        private readonly IPermissionManager _permissionManager;

        public MenuAppService(
            IRepository<Menu, Guid> repository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IMenuGrantRepository menuGrantRepository,
            IMenuManager menuManager,
            IPermissionManager permissionManager
        ) : base(repository)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
            _menuGrantRepository = menuGrantRepository;
            _menuManager = menuManager;
            _permissionManager = permissionManager;
        }

        protected override string GetListPolicyName => MenuManagementPermissions.Menus.Default;
        protected override string CreatePolicyName => MenuManagementPermissions.Menus.Create;
        protected override string UpdatePolicyName => MenuManagementPermissions.Menus.Update;
        protected override string DeletePolicyName => MenuManagementPermissions.Menus.Delete;

        public override async Task<MenuDto> UpdateAsync(Guid id, CreateOrUpdateMenuDto input)
        {
            PermissionChecker(input.PermissionKey);

            // 当更新菜单权限时，同时刷 PermissionGrant
            var menu = await GetEntityByIdAsync(id);
            if (menu.PermissionKey != input.PermissionKey)
            {
                await _menuManager.UpdatePermissionGrantAsync(id, menu.PermissionKey, input.PermissionKey);
            }

            return await base.UpdateAsync(id, input);
        }

        public override Task<MenuDto> CreateAsync(CreateOrUpdateMenuDto input)
        {
            PermissionChecker(input.PermissionKey);
            return base.CreateAsync(input);
        }

        public override Task<PagedResultDto<MenuDto>> GetListAsync(MenuRequestDto input)
        {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();

            var allMenus = Repository
                .WhereIf(input.Type.HasValue, m => m.MenuType == input.Type)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), m => m.DisplayName.Contains(input.Name))
                .WhereIf(multiTenancySide == MultiTenancySides.Tenant,
                    m => m.MultiTenancySide != MultiTenancySides.Host)
                .ToList();

            var root = allMenus
                .Where(x => !x.ParentId.HasValue) // 没有parentId
                .Union(
                    // 有parentId,但是“allMenus"中不存在他的Parent。
                    allMenus.Where(x => x.ParentId.HasValue).Where(x => allMenus.All(y => x.ParentId != y.Id))
                )
                .OrderBy(x => x.Sort);

            var menuDtos = new List<MenuDto>();
            foreach (var menu in root)
            {
                var dto = ObjectMapper.Map<Menu, MenuDto>(menu);
                menuDtos.Add(dto);
                // AddChildrenMenuRecursively(dto, allMenus);

                SortChildrenMenu(dto);
            }

            return Task.FromResult(new PagedResultDto<MenuDto>(allMenus.Count, menuDtos));
        }

        private void PermissionChecker(string permissionName)
        {
            if (!permissionName.IsNullOrWhiteSpace())
            {
                var permission = _permissionDefinitionManager.GetOrNull(permissionName);
                if (permission == null)
                {
                    throw new UserFriendlyException($"未知的权限:“{permissionName}”。");
                }
            }
        }

        private void SortChildrenMenu(MenuDto dto)
        {
            dto.Children.Sort((a, b) => string.Compare(a.Sort, b.Sort, StringComparison.Ordinal));
            dto.Children.ForEach(SortChildrenMenu);
        }

        private void AddChildrenMenuRecursively(MenuDto parent, List<Menu> allMenus)
        {
            foreach (var menu in allMenus.Where(x => x.ParentId == parent.Id).OrderBy(x => x.Sort))
            {
                var dto = ObjectMapper.Map<Menu, MenuDto>(menu);
                parent.Children.Add(dto);

                AddChildrenMenuRecursively(dto, allMenus);
            }
        }
    }
}