using Abp.VueTemplate.MenuManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate.MenuManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(MenuManagementEntityFrameworkCoreTestModule)
        )]
    public class MenuManagementDomainTestModule : AbpModule
    {
        
    }
}
