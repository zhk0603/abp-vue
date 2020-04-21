using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VueTemplate.MenuManagement
{
    public abstract class MenuManagementController : AbpController
    {
        protected MenuManagementController()
        {
            LocalizationResource = typeof(MenuManagementResource);
        }
    }
}
