using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    [DependsOn(
        typeof(PermissionDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class PermissionEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PermissionDbContext>(options =>
            {
                options.AddDefaultRepositories<IPermissionDbContext>(true);

                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}