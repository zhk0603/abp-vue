using Abp.VueTemplate.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.Web.Pages
{
    public abstract class VueTemplatePageModel : AbpPageModel
    {
        protected VueTemplatePageModel()
        {
            LocalizationResourceType = typeof(VueTemplateResource);
        }
    }
}