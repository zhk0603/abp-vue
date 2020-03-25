using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    [ConnectionStringName(PermissionDbProperties.ConnectionStringName)]
    public class PermissionDbContext : AbpDbContext<PermissionDbContext>, IPermissionDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePermission();
        }

        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<PermissionPage> PermissionPages { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}