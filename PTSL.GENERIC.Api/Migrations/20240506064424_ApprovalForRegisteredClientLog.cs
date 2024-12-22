using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalForRegisteredClientLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalForRegisteredClientLog",
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
                    ClientID = table.Column<long>(type: "bigint", nullable: true),
                    SenderId = table.Column<long>(type: "bigint", nullable: true),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    SendingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SendingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    SenderRoleId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalForRegisteredClientLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalForRegisteredClientLog_UserRoles_SenderRoleId",
                        column: x => x.SenderRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForRegisteredClientLog_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForRegisteredClientLog_User_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForRegisteredClientLog_ReceiverId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForRegisteredClientLog_SenderId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForRegisteredClientLog_SenderRoleId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "SenderRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalForRegisteredClientLog",
                schema: "GS");
        }
    }
}
