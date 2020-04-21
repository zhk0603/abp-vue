using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Abp.VueTemplate.MenuManagement;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore
{
    [ConnectionStringName(MenuManagementDbProperties.ConnectionStringName)]
    public interface IMenuManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Menu> Menus { get; set; }
        DbSet<MenuGrant> MenuGrants { get; set; }
    }
}
