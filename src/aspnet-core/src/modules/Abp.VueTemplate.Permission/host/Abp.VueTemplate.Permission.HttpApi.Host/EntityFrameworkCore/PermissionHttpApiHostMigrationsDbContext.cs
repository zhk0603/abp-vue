using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class PermissionHttpApiHostMigrationsDbContext : AbpDbContext<PermissionHttpApiHostMigrationsDbContext>
    {
        public PermissionHttpApiHostMigrationsDbContext(DbContextOptions<PermissionHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermission();
        }
    }
}
