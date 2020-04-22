using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid, MenuRequestDto,
            CreateOrUpdateMenuDto, CreateOrUpdateMenuDto>,
        IMenuAppService
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly IMenuManager _menuManager;
        private readonly IMenuGrantRepository _menuGrantRepository;
        private readonly IPermissionAppService _permissionAppService;

        public MenuAppService(IRepository<Menu, Guid> repository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IMenuManager menuManager,
            IMenuGrantRepository menuGrantRepository,
            IPermissionAppService permissionAppService) : base(repository)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
            _menuManager = menuManager;
            _menuGrantRepository = menuGrantRepository;
            _permissionAppService = permissionAppService;
        }

        public override Task<MenuDto> UpdateAsync(Guid id, CreateOrUpdateMenuDto input)
        {
            PermissionChecker(input.PermissionKey);
            return base.UpdateAsync(id, input);
        }

        public override Task<MenuDto> CreateAsync(CreateOrUpdateMenuDto input)
        {
            PermissionChecker(input.PermissionKey);
            return base.CreateAsync(input);
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

        public virtual Task<PagedResultDto<MenuDto>> GetAllListAsync(MenuRequestDto input)
        {
            var allMenus = Repository
                .WhereIf(input.Type.HasValue, m => m.MenuType == input.Type)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), m => m.DisplayName.Contains(input.Name))
                .ToList();

            var root = allMenus.Where(x => !x.ParentId.HasValue) // 没有parentId
                .Union(
                    // 有parentId,但是“allMenus"中不存在他的Parent。
                    allMenus.Where(x => x.ParentId.HasValue)
                        .Where(x => allMenus.All(y => x.ParentId != y.Id))
                )
                .OrderBy(x => x.Sort);

            var menuDtos = new List<MenuDto>();
            foreach(var menu in root)
            {
                var dto = ObjectMapper.Map<Menu, MenuDto>(menu);
                menuDtos.Add(dto);
                // AddChildrenMenuRecursively(dto, allMenus);

                SortChildrenMenu(dto);
            }

            return Task.FromResult(new PagedResultDto<MenuDto>(allMenus.Count, menuDtos));
        }

        public virtual async Task<GetMenuGrantListResultDto> GetGrantAsync(string providerName, string providerKey)
        {
            var allMenus = Repository.ToList();

            var menuGrants = new List<MenuGrantInfoDto>();

            foreach(var menu in allMenus)
            {
                var grantInfo = await _menuManager.GetAsync(menu.Id, providerName, providerKey);
                menuGrants.Add(new MenuGrantInfoDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    DisplayName = menu.DisplayName,
                    PermissionKey = menu.PermissionKey,
                    IsGranted = grantInfo.IsGranted,
                    Providers = grantInfo.Providers
                });
            }

            return new GetMenuGrantListResultDto()
            {
                MenuGrants = menuGrants
            };
        }

        public virtual async Task UpdateGrantAsync(string providerName, string providerKey, UpdateMenuGrantsDto input)
        {
            foreach (var grantDto in input.Menus)
            {
                await _menuManager.SetAsync(grantDto.Id, providerName, providerKey, grantDto.IsGranted);
            }

            var permissions = _menuManager.GetPermissions(providerName);
            var updatePermissionDtos = permissions
                .Select(x =>
                {
                    var menuDto = input.Menus.FirstOrDefault(y => y.PermissionKey == x.Name);
                    var dto = new UpdatePermissionDto
                    {
                        Name = x.Name,
                        IsGranted = menuDto?.IsGranted ?? false // 默认不给予授权，如果菜单与权限绑定，则以菜单的授权为主。
                    };

                    return dto;
                })
                .ToArray();

            await _permissionAppService.UpdateAsync(
                providerName,
                providerKey,
                new UpdatePermissionsDto
                {
                    Permissions = updatePermissionDtos
                }
            );
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
