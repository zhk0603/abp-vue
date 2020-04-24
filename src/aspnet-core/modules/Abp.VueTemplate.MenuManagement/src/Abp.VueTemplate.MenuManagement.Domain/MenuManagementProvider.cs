using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public abstract class MenuManagementProvider : IMenuManagementProvider
    {
        protected IMenuGrantRepository MenuGrantRepository { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IMenuGrantChecker MenuGrantChecker { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected ICurrentTenant CurrentTenant { get; }
        public abstract string Name { get; }

        protected MenuManagementProvider(IMenuGrantRepository menuGrantRepository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IMenuGrantChecker menuGrantChecker,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            MenuGrantRepository = menuGrantRepository;
            PermissionDefinitionManager = permissionDefinitionManager;
            MenuGrantChecker = menuGrantChecker;
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
        }

        public virtual async Task<MenuGrantInfo> CheckAsync(Guid menuId, string providerName, string providerKey)
        {
            if (providerName != Name)
            {
                return MenuGrantInfo.NonGranted;
            }

            return new MenuGrantInfo(
                (await MenuGrantChecker.CheckAsync(menuId, providerName, providerKey)).IsGranted,
                providerKey
            );
        }

        public virtual Task SetAsync(Guid menuId, string providerKey, bool isGranted)
        {
            return isGranted
                ? GrantAsync(menuId, providerKey)
                : RevokeAsync(menuId, providerKey);
        }

        protected virtual async Task GrantAsync(Guid menuId, string providerKey)
        {
            var menuGrant = await MenuGrantRepository.FindAsync(menuId, Name, providerKey);
            if (menuGrant != null)
            {
                return;
            }

            await MenuGrantRepository.InsertAsync(
                new MenuGrant(
                    GuidGenerator.Create(),
                    menuId,
                    Name,
                    providerKey,
                    CurrentTenant.Id
                )
            );
        }

        protected virtual async Task RevokeAsync(Guid menuId, string providerKey)
        {
            var permissionGrant = await MenuGrantRepository.FindAsync(menuId, Name, providerKey);
            if (permissionGrant == null)
            {
                return;
            }

            await MenuGrantRepository.DeleteAsync(permissionGrant);
        }
    }
}