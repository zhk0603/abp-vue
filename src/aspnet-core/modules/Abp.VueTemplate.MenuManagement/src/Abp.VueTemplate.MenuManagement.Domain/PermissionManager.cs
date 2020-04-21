using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.MenuManagement
{
    public class PermissionManager : IMenuManager, ISingletonDependency
    {
        public Task SetAsync(Guid menuId, string providerName, string providerKey, bool isGranted)
        {
            throw new NotImplementedException();
        }

        public Task<MenuGrant> UpdateProviderKeyAsync(MenuGrant menuGrant, string providerKey)
        {
            throw new NotImplementedException();
        }
    }
}