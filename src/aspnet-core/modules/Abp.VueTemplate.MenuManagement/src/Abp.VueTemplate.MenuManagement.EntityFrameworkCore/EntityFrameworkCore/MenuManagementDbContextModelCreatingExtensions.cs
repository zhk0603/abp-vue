using Abp.VueTemplate.MenuManagement;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore
{
    public static class MenuManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureMenuManagement(
            this ModelBuilder builder,
            Action<MenuManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MenuManagementModelBuilderConfigurationOptions(
                MenuManagementDbProperties.DbTablePrefix,
                MenuManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Menu>(b =>
            {
                b.ToTable(options.TablePrefix + "Menus", options.Schema);
                b.ConfigureByConvention();

                b.Property(x => x.Name).HasMaxLength(50);
                b.Property(x => x.DisplayName).HasMaxLength(50);
                b.Property(x => x.ComponentPath).HasMaxLength(100);
                b.Property(x => x.RouterPath).HasMaxLength(100);
                b.Property(x => x.Icon).HasMaxLength(50);
                b.Property(x => x.Sort).HasMaxLength(50);
                b.Property(x => x.TargetUrl).HasMaxLength(500);
                b.Property(x => x.PermissionKey).HasMaxLength(100);
            });

            builder.Entity<MenuGrant>(b =>
            {
                b.ToTable(options.TablePrefix + "MenuGrants", options.Schema);
                b.ConfigureByConvention();

                b.Property(x => x.MenuId).IsRequired();
                b.Property(x => x.ProviderName).HasMaxLength(MenuGrantConsts.MaxProviderNameLength).IsRequired();
                b.Property(x => x.ProviderKey).HasMaxLength(MenuGrantConsts.MaxProviderKeyLength).IsRequired();

                b.HasIndex(x => new {x.MenuId, x.ProviderName, x.ProviderKey});
            });
        }
    }
}
