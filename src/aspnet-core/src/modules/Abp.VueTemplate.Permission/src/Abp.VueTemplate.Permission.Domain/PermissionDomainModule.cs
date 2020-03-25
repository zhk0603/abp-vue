using System.Linq;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(PermissionDomainSharedModule)
        )]
    public class PermissionDomainModule : AbpModule
    {

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var permissionOptions = context.ServiceProvider.GetRequiredService<AbpPermissionOptions>();

            /*
             * 确保  PagePermissionDefinitionProvider 在最后一个。
             *  abp 的 PermissionGroupDefinition 构造函数是 internal 不能在外部初始化，所以通过继承 PermissionDefinitionManager 的方式行不通，
             *  只能通过 “DefinitionProvider”去附加我们自定义的“Permission”
             */ 
            var type = typeof(PagePermissionDefinitionProvider);
            if (permissionOptions.DefinitionProviders.Any(x=> x == type))
            {
                permissionOptions.DefinitionProviders.Remove(type);
            }
            permissionOptions.DefinitionProviders.Add(type);
        }
    }
}
