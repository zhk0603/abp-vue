using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementDomainSharedModule)
        )]
    public class MenuManagementDomainModule : AbpModule
    {

    }
}
