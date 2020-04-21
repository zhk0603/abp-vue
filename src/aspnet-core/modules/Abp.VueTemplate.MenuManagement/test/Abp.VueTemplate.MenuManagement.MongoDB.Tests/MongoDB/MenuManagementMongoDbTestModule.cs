using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement.MongoDB
{
    [DependsOn(
        typeof(MenuManagementTestBaseModule),
        typeof(MenuManagementMongoDbModule)
        )]
    public class MenuManagementMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}