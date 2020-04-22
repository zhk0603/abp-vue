using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using static System.String;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid, MenuRequestDto,
            CreateOrUpdateMenuDto, CreateOrUpdateMenuDto>,
        IMenuAppService
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public MenuAppService(IRepository<Menu, Guid> repository,
            IPermissionDefinitionManager permissionDefinitionManager) : base(repository)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
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

        //public override async Task<MenuDto> GetAsync(Guid id)
        //{
        //    await this.CheckGetPolicyAsync().ConfigureAwait(false);
        //    var entity = await GetEntityByIdAsync(id).ConfigureAwait(false);
        //    var dto = this.MapToGetOutputDto(entity);
        //    dto.ParentDisplayName = entity.Parent?.DisplayName;
        //    return dto;
        //}

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

        public Task<PagedResultDto<MenuDto>> GetAllListAsync(MenuRequestDto input)
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

        //public Task<MenuDto> CreatePermissionAsync(CreateOrUpdatePermissionDto input)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<MenuDto> UpdatePermissionAsync(Guid id, CreateOrUpdatePermissionDto input)
        //{
        //    throw new NotImplementedException();
        //}

        private void SortChildrenMenu(MenuDto dto)
        {
            dto.Children.Sort((a, b) => Compare(a.Sort, b.Sort, StringComparison.Ordinal));
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
