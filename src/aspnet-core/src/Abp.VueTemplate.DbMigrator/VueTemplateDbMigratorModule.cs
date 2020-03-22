using Abp.VueTemplate.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(VueTemplateEntityFrameworkCoreDbMigrationsModule),
        typeof(VueTemplateApplicationContractsModule)
        )]
    public class VueTemplateDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
