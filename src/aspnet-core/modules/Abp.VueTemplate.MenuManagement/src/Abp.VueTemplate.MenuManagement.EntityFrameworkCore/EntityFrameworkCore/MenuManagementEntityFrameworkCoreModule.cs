using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(MenuManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MenuManagementEntityFrameworkCoreModule : AbpModule
    {
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