using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.MenuManagement.Pages
{
    public abstract class MenuManagementPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<MenuManagementResource> L { get; set; }
    }
}
