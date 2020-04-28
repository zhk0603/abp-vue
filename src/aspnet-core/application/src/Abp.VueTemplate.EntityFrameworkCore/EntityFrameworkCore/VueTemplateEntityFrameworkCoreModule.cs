using Abp.VueTemplate.MenuManagement;
using Abp.VueTemplate.MenuManagement.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Guids;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Abp.VueTemplate.EntityFrameworkCore
{
    [DependsOn(
        typeof(MenuManagementEntityFrameworkCoreModule),
        typeof(VueTemplateDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
    public class VueTemplateEntityFrameworkCoreModule : AbpModule
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            MenuManagementDbProperties.DbTablePrefix = "App_";
            Volo.Abp.Data.AbpCommonDbProperties.DbTablePrefix = "Abp_";

            context.Services.AddAbpDbContext<VueTemplateDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpSequentialGuidGeneratorOptions>(options =>
            {
                // 切换DBMS时请注意此配置。
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also VueTemplateMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();

                // options.Configure(dbConfig =>
                //{
                //    dbConfig.DbContextOptions.UseLoggerFactory(MyLoggerFactory);
                //    dbConfig.DbContextOptions.EnableDetailedErrors();
                //    dbConfig.DbContextOptions.EnableSensitiveDataLogging();
                //});
            });
        }
    }
}
