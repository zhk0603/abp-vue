using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Abp.VueTemplate.Permission.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Abp.VueTemplate.Permission
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class PermissionDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PermissionDomainSharedModule>("Abp.VueTemplate.Permission");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PermissionResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Permission");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Permission", typeof(PermissionResource));
            });
        }
    }
}
