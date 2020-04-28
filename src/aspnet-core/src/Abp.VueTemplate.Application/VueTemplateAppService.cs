using System;
using System.Collections.Generic;
using System.Text;
using Abp.VueTemplate.Localization;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate
{
    /* Inherit your application services from this class.
     */
    public abstract class VueTemplateAppService : ApplicationService
    {
        protected VueTemplateAppService()
        {
            LocalizationResource = typeof(VueTemplateResource);
        }
    }
}
