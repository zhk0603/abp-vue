using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.EntityFrameworkCore
{
    [DependsOn(
        typeof(VueTemplateEntityFrameworkCoreModule)
        )]
    public class VueTemplateEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<VueTemplateMigrationsDbContext>();
        }
    }
}
