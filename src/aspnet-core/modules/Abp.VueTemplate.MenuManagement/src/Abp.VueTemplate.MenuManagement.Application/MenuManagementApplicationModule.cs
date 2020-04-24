using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(AbpPermissionManagementApplicationModule),
        typeof(MenuManagementDomainModule),
        typeof(MenuManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class MenuManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MenuManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<MenuManagementApplicationModule>(); });
        }
    }
}