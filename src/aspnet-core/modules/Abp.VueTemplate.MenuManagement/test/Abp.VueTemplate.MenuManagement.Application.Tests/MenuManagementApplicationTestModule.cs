using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementApplicationModule),
        typeof(MenuManagementDomainTestModule)
        )]
    public class MenuManagementApplicationTestModule : AbpModule
    {

    }
}
