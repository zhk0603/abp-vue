using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.MenuManagement.Pages
{
    public abstract class MenuManagementPageModel : AbpPageModel
    {
        protected MenuManagementPageModel()
        {
            LocalizationResourceType = typeof(MenuManagementResource);
        }
    }
}