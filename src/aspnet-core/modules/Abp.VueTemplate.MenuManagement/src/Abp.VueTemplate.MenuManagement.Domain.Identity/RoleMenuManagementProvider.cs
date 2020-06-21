using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Abp.VueTemplate.MenuManagement.Domain.Identity
{
    public class RoleMenuManagementProvider : MenuManagementProvider
    {
        private readonly IUserRoleFinder _userRoleFinder;

        public RoleMenuManagementProvider(
            IMenuGrantRepository menuGrantRepository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IMenuGrantChecker menuGrantChecker,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUserRoleFinder userRoleFinder
        ) :
            base(menuGrantRepository,
                permissionDefinitionManager,
                menuGrantChecker,
                guidGenerator,
                currentTenant
            )
        {
            _userRoleFinder = userRoleFinder;
        }

        public override string Name => RolePermissionValueProvider.ProviderName;

        public override async Task<MenuGrantInfo> CheckAsync(Guid menuId, string providerName,
            string providerKey)
        {
            if (providerName == Name)
            {
                return new MenuGrantInfo(
                    (await MenuGrantChecker.CheckAsync(menuId, providerName, providerKey)).IsGranted,
                    providerKey
                );
            }

            if (providerName == UserPermissionValueProvider.ProviderName)
            {
                var userId = Guid.Parse(providerKey);
                var roleNames = await _userRoleFinder.GetRolesAsync(userId);

                foreach (var roleName in roleNames)
                {
                    var grantCache = await MenuGrantChecker.CheckAsync(menuId, Name, roleName);
                    if (grantCache.IsGranted)
                    {
                        return new MenuGrantInfo(true, roleName);
                    }
                }
            }

            return MenuGrantInfo.NonGranted;
        }
    }
}