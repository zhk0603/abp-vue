using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class PermissionModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public PermissionModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}