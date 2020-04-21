using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VueTemplate.MenuManagement.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits Abp.VueTemplate.MenuManagement.Web.Pages.MenuManagementPage
     */
    public abstract class MenuManagementPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<MenuManagementResource> L { get; set; }
    }
}
