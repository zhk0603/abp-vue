using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.MenuManagement.Migrations
{
    public partial class AddSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "App_Menus",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "App_Menus",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "App_Menus",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "App_Menus");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "App_Menus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "App_Menus");
        }
    }
}
