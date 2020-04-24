using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.MenuManagement.Migrations
{
    public partial class MenuAddedMultiTenancySide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "App_Menus");

            migrationBuilder.AddColumn<int>(
                name: "MultiTenancySide",
                table: "App_Menus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultiTenancySide",
                table: "App_Menus");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "App_Menus",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
