using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.MenuManagement.MongoDB
{
    public static class MenuManagementMongoDbContextExtensions
    {
        public static void ConfigureMenuManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MenuManagementMongoModelBuilderConfigurationOptions(
                MenuManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}