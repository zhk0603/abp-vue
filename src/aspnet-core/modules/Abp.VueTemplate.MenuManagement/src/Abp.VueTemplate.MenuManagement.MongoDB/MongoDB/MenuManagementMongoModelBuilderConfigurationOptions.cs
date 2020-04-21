using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.MenuManagement.MongoDB
{
    public class MenuManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public MenuManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}