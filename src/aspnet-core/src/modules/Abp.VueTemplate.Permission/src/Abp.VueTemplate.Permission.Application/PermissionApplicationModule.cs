using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionDomainModule),
        typeof(PermissionApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class PermissionApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<PermissionApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PermissionApplicationModule>(validate: true);
            });
        }
    }
}
