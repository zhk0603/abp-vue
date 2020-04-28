using Abp.VueTemplate.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VueTemplate
{
    [DependsOn(
        typeof(VueTemplateEntityFrameworkCoreTestModule)
        )]
    public class VueTemplateDomainTestModule : AbpModule
    {

    }
}