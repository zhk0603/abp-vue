using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.VueTemplate.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.Web.Pages
{
    public abstract class VueTemplatePage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<VueTemplateResource> L { get; set; }
    }
}
