using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MenuManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
