using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.MenuManagement.Migrations
{
    public partial class CreatedMenuEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_MenuGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_MenuGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    MenuType = table.Column<int>(nullable: false),
                    ComponentPath = table.Column<string>(maxLength: 100, nullable: true),
                    RouterPath = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Sort = table.Column<string>(maxLength: 50, nullable: true),
                    TargetUrl = table.Column<string>(maxLength: 500, nullable: true),
                    PermissionKey = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_App_Menus_App_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "App_Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_MenuGrants_MenuId_ProviderName_ProviderKey",
                table: "App_MenuGrants",
                columns: new[] { "MenuId", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_App_Menus_ParentId",
                table: "App_Menus",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_MenuGrants");

            migrationBuilder.DropTable(
                name: "App_Menus");
        }
    }
}
