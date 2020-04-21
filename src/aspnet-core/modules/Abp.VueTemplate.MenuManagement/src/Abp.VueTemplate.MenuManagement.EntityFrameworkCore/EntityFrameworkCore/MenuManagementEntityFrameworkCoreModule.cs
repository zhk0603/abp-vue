using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(MenuManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MenuManagementEntityFrameworkCoreModule : AbpModule
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(dbConfig =>
                {
                    dbConfig.DbContextOptions.UseLoggerFactory(MyLoggerFactory);
                    dbConfig.DbContextOptions.EnableDetailedErrors();
                    dbConfig.DbContextOptions.EnableSensitiveDataLogging();
                });
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MenuManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                //options.AddDefaultRepositories();
                options.AddRepository<Menu, MenuRepository>();
            });
        }
    }
}