using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.MenuManagement.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class MenuManagementPageModel : AbpPageModel
    {
        protected MenuManagementPageModel()
        {
            LocalizationResourceType = typeof(MenuManagementResource);
            ObjectMapperContext = typeof(MenuManagementWebModule);
        }
    }
}