using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public static class PermissionDbContextModelCreatingExtensions
    {
        public static void ConfigurePermission(
            this ModelBuilder builder,
            Action<PermissionModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PermissionModelBuilderConfigurationOptions(
                PermissionDbProperties.DbTablePrefix,
                PermissionDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            // TODO 添加字段约束。

            builder.Entity<Menu>(x =>
            {
                x.ToTable(options.TablePrefix + "Menus", options.Schema);
                x.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

                x.ConfigureByConvention();
            });


            var group = builder.Entity<PermissionGroup>(x =>
            {
                x.ToTable(options.TablePrefix + "PermissionGroups", options.Schema);
                x.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
                x.HasMany(x => x.Permissions)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

                x.ConfigureByConvention();
            });

            var page = builder.Entity<PermissionPage>(x =>
            {

                x.ToTable(options.TablePrefix + "PermissionPages", options.Schema);
                x.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

                x.ConfigureByConvention();
            });

            builder.Entity<MenuGrant>(x =>
            {
                x.ToTable(options.TablePrefix + "MenuGrants", options.Schema);
                x.HasIndex(x => new { x.MenuId, x.ProviderKey, x.ProviderName });
            });
        }
    }
}