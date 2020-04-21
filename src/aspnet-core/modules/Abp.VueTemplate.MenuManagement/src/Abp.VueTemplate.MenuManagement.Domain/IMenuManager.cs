using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuManager
    {
        Task SetAsync(Guid menuId, string providerName, string providerKey, bool isGranted);

        Task<MenuGrant> UpdateProviderKeyAsync(MenuGrant menuGrant, string providerKey);
    }
}
