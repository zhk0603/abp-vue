using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionApplicationModule),
        typeof(PermissionDomainTestModule)
        )]
    public class PermissionApplicationTestModule : AbpModule
    {

    }
}
