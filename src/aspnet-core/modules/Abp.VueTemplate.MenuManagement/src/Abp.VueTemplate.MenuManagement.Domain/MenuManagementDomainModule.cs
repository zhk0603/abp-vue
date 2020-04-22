using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementDomainSharedModule),
        typeof(AbpPermissionManagementDomainModule)
        )]
    public class MenuManagementDomainModule : AbpModule
    {
    }
}
