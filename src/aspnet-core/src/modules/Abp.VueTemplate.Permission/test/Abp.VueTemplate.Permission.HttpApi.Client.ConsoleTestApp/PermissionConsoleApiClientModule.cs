using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class PermissionConsoleApiClientModule : AbpModule
    {
        
    }
}
