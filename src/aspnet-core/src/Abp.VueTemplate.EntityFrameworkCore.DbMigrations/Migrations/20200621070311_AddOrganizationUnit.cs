using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.Migrations
{
    public partial class AddOrganizationUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp_OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_OrganizationUnits_Abp_OrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Abp_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abp_OrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_OrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Abp_OrganizationUnitRoles_Abp_OrganizationUnits_Organization~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Abp_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp_OrganizationUnitRoles_Abp_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Abp_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_UserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_UserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Abp_UserOrganizationUnits_Abp_OrganizationUnits_Organization~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Abp_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp_UserOrganizationUnits_Abp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Abp_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_Menus_PermissionKey",
                table: "App_Menus",
                column: "PermissionKey");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_OrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "Abp_OrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_OrganizationUnits_Code",
                table: "Abp_OrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_OrganizationUnits_ParentId",
                table: "Abp_OrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_UserOrganizationUnits_UserId_OrganizationUnitId",
                table: "Abp_UserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abp_OrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "Abp_UserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "Abp_OrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_App_Menus_PermissionKey",
                table: "App_Menus");
        }
    }
}
