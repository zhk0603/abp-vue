using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuGrantChecker
    {
        Task<MenuGrantCacheItem> CheckAsync(Guid menuId, string providerName, string providerKey);
    }
}
