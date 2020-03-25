using Abp.VueTemplate.Permission.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.Permission
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(PermissionEntityFrameworkCoreTestModule)
        )]
    public class PermissionDomainTestModule : AbpModule
    {
        
    }
}
