using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.Migrations
{
    public partial class AddPermissionModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionMenuGrants",
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
                    table.PrimaryKey("PK_PermissionMenuGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionMenus",
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
                    table.PrimaryKey("PK_PermissionMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionMenus_PermissionMenus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PermissionMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPermissionGroups",
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
                    table.PrimaryKey("PK_PermissionPermissionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionPermissionGroups_PermissionPermissionGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PermissionPermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPermissionPages",
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
                    table.PrimaryKey("PK_PermissionPermissionPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionPermissionPages_PermissionPermissionGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PermissionPermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionPermissionPages_PermissionPermissionPages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PermissionPermissionPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenuGrants_MenuId_ProviderKey_ProviderName",
                table: "PermissionMenuGrants",
                columns: new[] { "MenuId", "ProviderKey", "ProviderName" });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenus_ParentId",
                table: "PermissionMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPermissionGroups_ParentId",
                table: "PermissionPermissionGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPermissionPages_GroupId",
                table: "PermissionPermissionPages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPermissionPages_ParentId",
                table: "PermissionPermissionPages",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionMenuGrants");

            migrationBuilder.DropTable(
                name: "PermissionMenus");

            migrationBuilder.DropTable(
                name: "PermissionPermissionPages");

            migrationBuilder.DropTable(
                name: "PermissionPermissionGroups");
        }
    }
}
