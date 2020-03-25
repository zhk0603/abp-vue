using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.Permission.MongoDB
{
    public class PermissionMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public PermissionMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}