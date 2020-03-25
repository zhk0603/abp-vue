using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.VueTemplate.Permission.MongoDB
{
    [ConnectionStringName(PermissionDbProperties.ConnectionStringName)]
    public interface IPermissionMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
