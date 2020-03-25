using Abp.VueTemplate.Permission.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VueTemplate.Permission
{
    public abstract class PermissionController : AbpController
    {
        protected PermissionController()
        {
            LocalizationResource = typeof(PermissionResource);
        }
    }
}
