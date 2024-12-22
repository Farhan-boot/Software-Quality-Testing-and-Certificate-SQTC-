using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "System");

            migrationBuilder.EnsureSchema(
                name: "GS");

            migrationBuilder.EnsureSchema(
                name: "PermissionSettings");

            migrationBuilder.EnsureSchema(
                name: "Archive");

            migrationBuilder.CreateTable(
                name: "AccessMapper",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AccessList = table.Column<string>(type: "varchar(500)", nullable: false),
                    RoleStatus = table.Column<byte>(type: "smallint", nullable: false),
                    IsVisible = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessMapper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "GS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModuleName = table.Column<string>(type: "varchar(500)", nullable: false),
                    ModuleIcon = table.Column<string>(type: "varchar(500)", nullable: false),
                    IsVisible = table.Column<byte>(type: "smallint", nullable: false),
                    MenueOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PmsGroup",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(40)", nullable: false),
                    GroupDescription = table.Column<string>(type: "varchar(250)", nullable: false),
                    GroupStatus = table.Column<byte>(type: "smallint", nullable: false),
                    IsVisible = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmsGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RememberMe = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    DeviceInfo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationArchive",
                schema: "Archive",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ArchiveName = table.Column<string>(type: "varchar(500)", nullable: true),
                    DocumentDescription = table.Column<string>(type: "varchar(500)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationArchive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(40)", nullable: false),
                    ModuleActionNames = table.Column<string>(type: "varchar(500)", nullable: false),
                    IsUsed = table.Column<byte>(type: "smallint", nullable: false),
                    IsDefault = table.Column<byte>(type: "smallint", nullable: false),
                    IsVisible = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(500)", nullable: false),
                    AccessList = table.Column<string>(type: "varchar(900)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accesslist",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ControllerName = table.Column<string>(type: "varchar(500)", nullable: false),
                    ActionName = table.Column<string>(type: "varchar(500)", nullable: true),
                    Mask = table.Column<string>(type: "varchar(500)", nullable: false),
                    AccessStatus = table.Column<byte>(type: "smallint", nullable: false),
                    IsVisible = table.Column<byte>(type: "smallint", nullable: false),
                    IconClass = table.Column<string>(type: "varchar(500)", nullable: true),
                    BaseModuleIndex = table.Column<int>(type: "int", nullable: true),
                    ModuleId = table.Column<long>(type: "bigint", nullable: true),
                    IsAvailableForApproval = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesslist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accesslist_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "System",
                        principalTable: "Module",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistrationArchiveFile",
                schema: "Archive",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RegistrationArchiveId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "varchar(500)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "varchar(500)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationArchiveFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationArchiveFile_RegistrationArchive_RegistrationArc~",
                        column: x => x.RegistrationArchiveId,
                        principalSchema: "Archive",
                        principalTable: "RegistrationArchive",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    RoleName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(500)", nullable: false),
                    UserEmail = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(500)", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    UserPhone = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserGroup = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: true),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalSchema: "System",
                        principalTable: "UserGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRolePermissionMap",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolePermissionMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRolePermissionMap_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionHeaderSettings",
                schema: "PermissionSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    AccesslistId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionHeaderSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionHeaderSettings_Accesslist_AccesslistId",
                        column: x => x.AccesslistId,
                        principalSchema: "System",
                        principalTable: "Accesslist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionHeaderSettings_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermissionHeaderSettings_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PermissionRowSettings",
                schema: "PermissionSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionHeaderSettingsId = table.Column<long>(type: "bigint", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRowSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionRowSettings_PermissionHeaderSettings_PermissionHe~",
                        column: x => x.PermissionHeaderSettingsId,
                        principalSchema: "PermissionSettings",
                        principalTable: "PermissionHeaderSettings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermissionRowSettings_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermissionRowSettings_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesslist_ModuleId",
                schema: "System",
                table: "Accesslist",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionHeaderSettings_AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                column: "AccesslistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionHeaderSettings_UserId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionHeaderSettings_UserRoleId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRowSettings_PermissionHeaderSettingsId",
                schema: "PermissionSettings",
                table: "PermissionRowSettings",
                column: "PermissionHeaderSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRowSettings_UserId",
                schema: "PermissionSettings",
                table: "PermissionRowSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRowSettings_UserRoleId",
                schema: "PermissionSettings",
                table: "PermissionRowSettings",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "System",
                table: "RefreshToken",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationArchiveFile_RegistrationArchiveId",
                schema: "Archive",
                table: "RegistrationArchiveFile",
                column: "RegistrationArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserGroupId",
                schema: "System",
                table: "User",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                schema: "System",
                table: "User",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermissionMap_UserRoleId",
                schema: "System",
                table: "UserRolePermissionMap",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessMapper",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "GS");

            migrationBuilder.DropTable(
                name: "PermissionRowSettings",
                schema: "PermissionSettings");

            migrationBuilder.DropTable(
                name: "PmsGroup",
                schema: "System");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "System");

            migrationBuilder.DropTable(
                name: "RegistrationArchiveFile",
                schema: "Archive");

            migrationBuilder.DropTable(
                name: "UserRolePermissionMap",
                schema: "System");

            migrationBuilder.DropTable(
                name: "PermissionHeaderSettings",
                schema: "PermissionSettings");

            migrationBuilder.DropTable(
                name: "RegistrationArchive",
                schema: "Archive");

            migrationBuilder.DropTable(
                name: "Accesslist",
                schema: "System");

            migrationBuilder.DropTable(
                name: "User",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "System");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "System");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "System");
        }
    }
}
