using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class PermissionHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<PermissionHttpApiHostMigrationsDbContext>
    {
        public PermissionHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<PermissionHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Permission"));

            return new PermissionHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
