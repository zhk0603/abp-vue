using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    [ConnectionStringName(PermissionDbProperties.ConnectionStringName)]
    public interface IPermissionDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}