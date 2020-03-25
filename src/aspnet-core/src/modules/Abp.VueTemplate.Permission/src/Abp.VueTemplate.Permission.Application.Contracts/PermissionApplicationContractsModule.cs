using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class PermissionApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PermissionApplicationContractsModule>("Abp.VueTemplate.Permission");
            });
        }
    }
}
