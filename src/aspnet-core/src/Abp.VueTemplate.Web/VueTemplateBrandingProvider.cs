using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.Web
{
    [Dependency(ReplaceServices = true)]
    public class VueTemplateBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "VueTemplate";
    }
}
