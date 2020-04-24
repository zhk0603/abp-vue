using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Security.Claims;

namespace Abp.VueTemplate.MenuManagement
{
    [Authorize]
    public class MenuGrantAppService : ApplicationService, IMenuGrantAppService
    {
        private readonly IAbpAuthorizationPolicyProvider _abpAuthorizationPolicyProvider;
        private readonly IUserMenuGrantChecker _userMenuGrantChecker;
        private readonly ICurrentPrincipalAccessor _principalAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMenuManager _menuManager;
        private readonly IMenuRepository _menuRepository;
        private readonly PermissionManagementOptions _options;
        private readonly IPermissionAppService _permissionAppService;

        public MenuGrantAppService(
            IMenuRepository menuRepository,
            IMenuManager menuManager,
            IPermissionAppService permissionAppService,
            IOptions<PermissionManagementOptions> options,
            IAuthorizationService authorizationService,
            IAbpAuthorizationPolicyProvider abpAuthorizationPolicyProvider,
            IUserMenuGrantChecker userMenuGrantChecker,
            ICurrentPrincipalAccessor principalAccessor
        )
        {
            _menuRepository = menuRepository;
            _menuManager = menuManager;
            _permissionAppService = permissionAppService;
            _authorizationService = authorizationService;
            _abpAuthorizationPolicyProvider = abpAuthorizationPolicyProvider;
            _userMenuGrantChecker = userMenuGrantChecker;
            _principalAccessor = principalAccessor;
            _options = options.Value;
        }

        public virtual async Task<GetMenuResultDto> GetListAsync()
        {
            var rootMenus = _menuRepository.Where(
                    x => x.MenuType == MenuEnumType.Menu
                )
                .OrderBy(x => x.Sort)
                .ToList()
                .Where(x=> !x.ParentId.HasValue).ToList(); // 根节点

            var menuDtos = new List<VueMenu>();
            foreach (var menu in rootMenus)
            {
                var isGranted = await _userMenuGrantChecker.CheckAsync(_principalAccessor.Principal, menu);
                if (isGranted)
                {
                    var dto = ObjectMapper.Map<Menu, VueMenu>(menu);
                    menuDtos.Add(dto);
                    FilterChildrenMenuRecursively(menu, dto);
                }
            }

            var permissionGrants = new List<string>();
            var policyNames = await _abpAuthorizationPolicyProvider.GetPoliciesNamesAsync();
            foreach (var policyName in policyNames)
            {
                if (await _authorizationService.IsGrantedAsync(policyName))
                {
                    permissionGrants.Add(policyName);
                }
            }

            return new GetMenuResultDto
            {
                Menus = menuDtos,
                PermissionGrants = permissionGrants
            };
        }

        public virtual async Task<GetMenuGrantListResultDto> GetAsync(string providerName, string providerKey)
        {
            await CheckProviderPolicy(providerName);
            return await InternalGetAsync(providerName, providerKey);
        }
    
        public virtual async Task UpdateAsync(string providerName, string providerKey, UpdateMenuGrantsDto input)
        {
            await CheckProviderPolicy(providerName);

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

        private async Task<GetMenuGrantListResultDto> InternalGetAsync(string providerName, string providerKey)
        {
            var allMenus = _menuRepository.ToList();
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();

            var menuGrants = new List<MenuGrantInfoDto>();

            foreach (var menu in allMenus)
            {
                if (!menu.MultiTenancySide.HasFlag(multiTenancySide))
                {
                    continue;
                }

                var grantInfo = await _menuManager.GetAsync(menu.Id, providerName, providerKey);
                menuGrants.Add(new MenuGrantInfoDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    DisplayName = menu.DisplayName,
                    PermissionKey = menu.PermissionKey,
                    IsGranted = grantInfo.IsGranted,
                    GrantedProviders = grantInfo.Providers
                });
            }

            return new GetMenuGrantListResultDto
            {
                MenuGrants = menuGrants
            };
        }

        private async Task CheckProviderPolicy(string providerName)
        {
            var policyName = _options.ProviderPolicies.GetOrDefault(providerName);
            if (policyName.IsNullOrEmpty())
            {
                throw new AbpException(
                    $"No policy defined to get/set permissions for the provider '{policyName}'. Use {nameof(PermissionManagementOptions)} to map the policy.");
            }

            await AuthorizationService.CheckAsync(policyName);
        }

        private void FilterChildrenMenuRecursively(Menu parent,VueMenu vueMenu)
        {
            if (parent.Children != null)
            {
                vueMenu.Children = new List<VueMenu>();
                foreach (var menu in parent.Children.Where( x => _userMenuGrantChecker.CheckAsync(_principalAccessor.Principal, x).Result))
                {
                    var dto = ObjectMapper.Map<Menu, VueMenu>(menu);
                    vueMenu.Children.Add(dto);
                    FilterChildrenMenuRecursively(menu, dto);
                }
            }
        }
    }
}