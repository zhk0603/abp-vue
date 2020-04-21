using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.MenuManagement.MongoDB
{
    [ConnectionStringName(MenuManagementDbProperties.ConnectionStringName)]
    public class MenuManagementMongoDbContext : AbpMongoDbContext, IMenuManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureMenuManagement();
        }
    }
}