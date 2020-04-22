using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuManager
    {
        IReadOnlyList<PermissionDefinition> GetPermissions(string providerName);
        Task<MenuWithGrantedProviders> GetAsync(Guid menuId, string providerName, string providerKey);
        Task SetAsync(Guid menuId, string providerName, string providerKey, bool isGranted);
    }
}
