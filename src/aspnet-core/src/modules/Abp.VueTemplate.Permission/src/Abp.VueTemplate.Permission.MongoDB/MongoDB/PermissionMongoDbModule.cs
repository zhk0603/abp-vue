using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.Permission.MongoDB
{
    [DependsOn(
        typeof(PermissionDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class PermissionMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<PermissionMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
