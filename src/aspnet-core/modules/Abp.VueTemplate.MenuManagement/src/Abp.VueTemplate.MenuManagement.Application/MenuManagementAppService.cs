using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate.MenuManagement
{
    public abstract class MenuManagementAppService : ApplicationService
    {
        protected MenuManagementAppService()
        {
            LocalizationResource = typeof(MenuManagementResource);
            ObjectMapperContext = typeof(MenuManagementApplicationModule);
        }
    }
}