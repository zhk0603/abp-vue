using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionDomainSharedModule)
        )]
    public class PermissionDomainModule : AbpModule
    {

    }
}
