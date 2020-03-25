using Microsoft.EntityFrameworkCore;
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

        DbSet<PermissionGroup> PermissionGroups { get; set; }
        DbSet<PermissionPage> PermissionPages { get; set; }
        DbSet<Menu> Menus { get; set; }
    }
}