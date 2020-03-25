using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class PermissionHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Permission";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(PermissionApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
