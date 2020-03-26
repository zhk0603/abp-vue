using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.Permission.Migrations
{
    public partial class MenuGrant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuType",
                table: "Menu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PermissionKey",
                table: "Menu",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuGrant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: false),
                    ProviderName = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGrant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuGrant_MenuId_ProviderKey_ProviderName",
                table: "MenuGrant",
                columns: new[] { "MenuId", "ProviderKey", "ProviderName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuGrant");

            migrationBuilder.DropColumn(
                name: "MenuType",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "PermissionKey",
                table: "Menu");
        }
    }
}
