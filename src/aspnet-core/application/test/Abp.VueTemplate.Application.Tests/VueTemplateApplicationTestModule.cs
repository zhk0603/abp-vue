using Volo.Abp.Modularity;

namespace Abp.VueTemplate
{
    [DependsOn(
        typeof(VueTemplateApplicationModule),
        typeof(VueTemplateDomainTestModule)
        )]
    public class VueTemplateApplicationTestModule : AbpModule
    {

    }
}