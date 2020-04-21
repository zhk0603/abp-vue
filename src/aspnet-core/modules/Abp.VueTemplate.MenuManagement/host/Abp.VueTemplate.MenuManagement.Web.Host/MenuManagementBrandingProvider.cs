using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.MenuManagement
{
    [Dependency(ReplaceServices = true)]
    public class MenuManagementBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MenuManagement";
    }
}
