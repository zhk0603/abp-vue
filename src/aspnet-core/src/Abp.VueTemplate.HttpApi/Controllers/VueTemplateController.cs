using Abp.VueTemplate.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VueTemplate.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class VueTemplateController : AbpController
    {
        protected VueTemplateController()
        {
            LocalizationResource = typeof(VueTemplateResource);
        }
    }
}