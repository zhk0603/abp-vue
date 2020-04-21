using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.MenuManagement.MongoDB
{
    [ConnectionStringName(MenuManagementDbProperties.ConnectionStringName)]
    public interface IMenuManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
