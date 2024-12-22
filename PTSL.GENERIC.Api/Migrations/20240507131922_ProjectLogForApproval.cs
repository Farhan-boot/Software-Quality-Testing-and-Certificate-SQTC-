using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectLogForApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalForProjectLog",
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
                    ProjectID = table.Column<long>(type: "bigint", nullable: true),
                    SenderId = table.Column<long>(type: "bigint", nullable: true),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    SendingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SendingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    SenderRoleId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProcessFlowType = table.Column<int>(type: "integer", nullable: false),
                    PermissionRowSettingsId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalForProjectLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalForProjectLog_PermissionRowSettings_PermissionRowSe~",
                        column: x => x.PermissionRowSettingsId,
                        principalSchema: "PermissionSettings",
                        principalTable: "PermissionRowSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalForProjectLog_UserRoles_SenderRoleId",
                        column: x => x.SenderRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForProjectLog_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForProjectLog_User_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForProjectLog_PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "PermissionRowSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForProjectLog_ReceiverId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForProjectLog_SenderId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForProjectLog_SenderRoleId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "SenderRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalForProjectLog",
                schema: "GS");
        }
    }
}
