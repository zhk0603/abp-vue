using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Abp.VueTemplate.MenuManagement
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(AbpPermissionManagementDomainSharedModule)
    )]
    public class MenuManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MenuManagementDomainSharedModule>("Abp.VueTemplate.MenuManagement");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MenuManagementResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/MenuManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("MenuManagement", typeof(MenuManagementResource));
            });
        }
    }
}
