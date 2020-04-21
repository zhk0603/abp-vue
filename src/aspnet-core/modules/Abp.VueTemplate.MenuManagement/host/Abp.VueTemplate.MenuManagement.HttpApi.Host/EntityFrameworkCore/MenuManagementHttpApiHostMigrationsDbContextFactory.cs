using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore
{
    public class MenuManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MenuManagementHttpApiHostMigrationsDbContext>
    {
        public MenuManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MenuManagementHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("MenuManagement"));

            return new MenuManagementHttpApiHostMigrationsDbContext(builder.Options);
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
