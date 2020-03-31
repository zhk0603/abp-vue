using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.Migrations
{
    public partial class AddPermissionModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppMenuGrants",
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
                    table.PrimaryKey("PK_AppMenuGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    MenuType = table.Column<int>(nullable: false),
                    ComponentPath = table.Column<string>(nullable: true),
                    RouterPath = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Sort = table.Column<string>(nullable: true),
                    TargetUrl = table.Column<string>(nullable: true),
                    PermissionKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMenus_AppMenus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppPermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermissionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPermissionGroups_AppPermissionGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppPermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppPermissionPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermissionPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPermissionPages_AppPermissionGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AppPermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPermissionPages_AppPermissionPages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppPermissionPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuGrants_MenuId_ProviderKey_ProviderName",
                table: "AppMenuGrants",
                columns: new[] { "MenuId", "ProviderKey", "ProviderName" });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenus_ParentId",
                table: "AppMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPermissionGroups_ParentId",
                table: "AppPermissionGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPermissionPages_GroupId",
                table: "AppPermissionPages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPermissionPages_ParentId",
                table: "AppPermissionPages",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMenuGrants");

            migrationBuilder.DropTable(
                name: "AppMenus");

            migrationBuilder.DropTable(
                name: "AppPermissionPages");

            migrationBuilder.DropTable(
                name: "AppPermissionGroups");
        }
    }
}
