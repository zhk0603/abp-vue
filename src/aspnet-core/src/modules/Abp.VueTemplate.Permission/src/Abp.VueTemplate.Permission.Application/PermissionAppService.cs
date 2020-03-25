using Abp.VueTemplate.Permission.Localization;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate.Permission
{
    public abstract class PermissionAppService : ApplicationService
    {
        protected PermissionAppService()
        {
            LocalizationResource = typeof(PermissionResource);
            ObjectMapperContext = typeof(PermissionApplicationModule);
        }
    }
}
