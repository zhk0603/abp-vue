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

            builder.Entity<Menu>(b =>
            {
                b.ToTable(options.TablePrefix + "Menus", options.Schema);
                b.HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId);

                b.ConfigureByConvention();
            });


            builder.Entity<PermissionGroup>(b =>
            {
                b.ToTable(options.TablePrefix + "PermissionGroups", options.Schema);
                b.HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId);
                b.HasMany(x => x.Permissions)
                    .WithOne(x => x.Group)
                    .HasForeignKey(x => x.GroupId);

                b.ConfigureByConvention();
            });

            builder.Entity<PermissionPage>(b =>
            {

                b.ToTable(options.TablePrefix + "PermissionPages", options.Schema);
                b.HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId);

                b.ConfigureByConvention();
            });

            builder.Entity<MenuGrant>(b =>
            {
                b.ToTable(options.TablePrefix + "MenuGrants", options.Schema);
                b.HasIndex(x => new {x.MenuId, x.ProviderKey, x.ProviderName});
            });
        }
    }
}