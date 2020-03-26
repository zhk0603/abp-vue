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
                options.AddRepository<Menu, MenuRepository>();
                options.AddRepository<PermissionGroup, PermissionGroupRepository>();
                options.AddRepository<PermissionPage, PermissionPageRepository>();
            });
        }
    }
}