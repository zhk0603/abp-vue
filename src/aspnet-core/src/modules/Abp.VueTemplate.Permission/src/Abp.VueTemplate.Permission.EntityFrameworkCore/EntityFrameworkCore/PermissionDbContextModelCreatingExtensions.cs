using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

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

            builder.Entity<Menu>()
                .HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

            var group = builder.Entity<PermissionGroup>();
            group.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
            group.HasMany(x => x.Permissions)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            var page = builder.Entity<PermissionPage>();
            page.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

        }
    }
}