using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.Permission.MongoDB
{
    [ConnectionStringName(PermissionDbProperties.ConnectionStringName)]
    public class PermissionMongoDbContext : AbpMongoDbContext, IPermissionMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigurePermission();
        }
    }
}