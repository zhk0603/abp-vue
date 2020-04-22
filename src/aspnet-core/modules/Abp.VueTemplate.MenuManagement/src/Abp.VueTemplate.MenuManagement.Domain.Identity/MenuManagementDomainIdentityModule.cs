using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement.Domain.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(MenuManagementDomainModule)
    )]
    public class MenuManagementDomainIdentityModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<MenuManagementOptions>(options =>
            {
                options.ManagementProviders.Add<RoleMenuManagementProvider>();
                options.ManagementProviders.Add<UserMenuManagementProvider>();
            });
        }
    }
}
