using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(VueTemplateHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class VueTemplateConsoleApiClientModule : AbpModule
    {
        
    }
}
