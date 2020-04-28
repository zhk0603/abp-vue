using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.VueTemplate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp_AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    ApplicationName = table.Column<string>(maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TenantName = table.Column<string>(nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(nullable: true),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(maxLength: 16, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true),
                    Exceptions = table.Column<string>(maxLength: 4000, nullable: true),
                    Comments = table.Column<string>(maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_BackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(maxLength: 128, nullable: false),
                    JobArgs = table.Column<string>(maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(nullable: false, defaultValue: (short)0),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    NextTryTime = table.Column<DateTime>(nullable: false),
                    LastTryTime = table.Column<DateTime>(nullable: true),
                    IsAbandoned = table.Column<bool>(nullable: false, defaultValue: false),
                    Priority = table.Column<byte>(nullable: false, defaultValue: (byte)15)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_BackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    Regex = table.Column<string>(maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_FeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_FeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_PermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_PermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 256, nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_Tenants",
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
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abp_Users",
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
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Surname = table.Column<string>(maxLength: 64, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_Users", x => x.Id);
                });

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
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    MenuType = table.Column<int>(nullable: false),
                    ComponentPath = table.Column<string>(maxLength: 100, nullable: true),
                    RouterPath = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Sort = table.Column<string>(maxLength: 50, nullable: true),
                    TargetUrl = table.Column<string>(maxLength: 500, nullable: true),
                    PermissionKey = table.Column<string>(maxLength: 100, nullable: true),
                    MultiTenancySide = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Abp_AuditLogActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    AuditLogId = table.Column<Guid>(nullable: false),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_AuditLogActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_AuditLogActions_Abp_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "Abp_AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_EntityChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuditLogId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ChangeTime = table.Column<DateTime>(nullable: false),
                    ChangeType = table.Column<byte>(nullable: false),
                    EntityTenantId = table.Column<Guid>(nullable: true),
                    EntityId = table.Column<string>(maxLength: 128, nullable: false),
                    EntityTypeFullName = table.Column<string>(maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_EntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_EntityChanges_Abp_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "Abp_AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_RoleClaims_Abp_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Abp_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_TenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Value = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_TenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_Abp_TenantConnectionStrings_Abp_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Abp_Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_UserClaims_Abp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Abp_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ProviderKey = table.Column<string>(maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_UserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_Abp_UserLogins_Abp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Abp_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Abp_UserRoles_Abp_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Abp_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp_UserRoles_Abp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Abp_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Abp_UserTokens_Abp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Abp_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp_EntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    EntityChangeId = table.Column<Guid>(nullable: false),
                    NewValue = table.Column<string>(maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp_EntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp_EntityPropertyChanges_Abp_EntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "Abp_EntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_AuditLogActions_AuditLogId",
                table: "Abp_AuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_AuditLogActions_TenantId_ServiceName_MethodName_Executio~",
                table: "Abp_AuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_AuditLogs_TenantId_ExecutionTime",
                table: "Abp_AuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_AuditLogs_TenantId_UserId_ExecutionTime",
                table: "Abp_AuditLogs",
                columns: new[] { "TenantId", "UserId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_BackgroundJobs_IsAbandoned_NextTryTime",
                table: "Abp_BackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_EntityChanges_AuditLogId",
                table: "Abp_EntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_EntityChanges_TenantId_EntityTypeFullName_EntityId",
                table: "Abp_EntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_EntityPropertyChanges_EntityChangeId",
                table: "Abp_EntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_FeatureValues_Name_ProviderName_ProviderKey",
                table: "Abp_FeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_PermissionGrants_Name_ProviderName_ProviderKey",
                table: "Abp_PermissionGrants",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_RoleClaims_RoleId",
                table: "Abp_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Roles_NormalizedName",
                table: "Abp_Roles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Settings_Name_ProviderName_ProviderKey",
                table: "Abp_Settings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Tenants_Name",
                table: "Abp_Tenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_UserClaims_UserId",
                table: "Abp_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_UserLogins_LoginProvider_ProviderKey",
                table: "Abp_UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_UserRoles_RoleId_UserId",
                table: "Abp_UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Users_Email",
                table: "Abp_Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Users_NormalizedEmail",
                table: "Abp_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Users_NormalizedUserName",
                table: "Abp_Users",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Abp_Users_UserName",
                table: "Abp_Users",
                column: "UserName");

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
                name: "Abp_AuditLogActions");

            migrationBuilder.DropTable(
                name: "Abp_BackgroundJobs");

            migrationBuilder.DropTable(
                name: "Abp_ClaimTypes");

            migrationBuilder.DropTable(
                name: "Abp_EntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "Abp_FeatureValues");

            migrationBuilder.DropTable(
                name: "Abp_PermissionGrants");

            migrationBuilder.DropTable(
                name: "Abp_RoleClaims");

            migrationBuilder.DropTable(
                name: "Abp_Settings");

            migrationBuilder.DropTable(
                name: "Abp_TenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "Abp_UserClaims");

            migrationBuilder.DropTable(
                name: "Abp_UserLogins");

            migrationBuilder.DropTable(
                name: "Abp_UserRoles");

            migrationBuilder.DropTable(
                name: "Abp_UserTokens");

            migrationBuilder.DropTable(
                name: "App_MenuGrants");

            migrationBuilder.DropTable(
                name: "App_Menus");

            migrationBuilder.DropTable(
                name: "Abp_EntityChanges");

            migrationBuilder.DropTable(
                name: "Abp_Tenants");

            migrationBuilder.DropTable(
                name: "Abp_Roles");

            migrationBuilder.DropTable(
                name: "Abp_Users");

            migrationBuilder.DropTable(
                name: "Abp_AuditLogs");
        }
    }
}
