using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Abp.VueTemplate.EntityFrameworkCore
{
    public class IdentityServerMigrationsDbContext : AbpDbContext<IdentityServerMigrationsDbContext>
    {
        public IdentityServerMigrationsDbContext(DbContextOptions<IdentityServerMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentityServer(options =>
            {
                options.DatabaseProvider = EfCoreDatabaseProvider.MySql;
            });
        }
    }
}