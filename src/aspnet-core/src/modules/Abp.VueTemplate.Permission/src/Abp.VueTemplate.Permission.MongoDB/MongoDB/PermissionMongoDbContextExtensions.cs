using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.Permission.MongoDB
{
    public static class PermissionMongoDbContextExtensions
    {
        public static void ConfigurePermission(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PermissionMongoModelBuilderConfigurationOptions(
                PermissionDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}