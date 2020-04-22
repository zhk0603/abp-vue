using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement.Domain.Identity
{
    public class UserMenuManagementProvider : MenuManagementProvider
    {
        public UserMenuManagementProvider(IMenuGrantRepository menuGrantRepository, IPermissionDefinitionManager permissionDefinitionManager, IGuidGenerator guidGenerator, ICurrentTenant currentTenant) : base(menuGrantRepository, permissionDefinitionManager, guidGenerator, currentTenant)
        {
        }

        public override string Name => UserPermissionValueProvider.ProviderName;
    }
}