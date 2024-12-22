using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalForAllDocument_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalForAllDocument",
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
                    AllTypesOfDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: true),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    SendingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    SenderRoleId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProcessFlowType = table.Column<int>(type: "integer", nullable: false),
                    DocumentType = table.Column<int>(type: "integer", nullable: true),
                    TestingType = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalForAllDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalForAllDocument_AllTypesOfDocument_AllTypesOfDocumen~",
                        column: x => x.AllTypesOfDocumentId,
                        principalSchema: "GS",
                        principalTable: "AllTypesOfDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalForAllDocument_UserRoles_SenderRoleId",
                        column: x => x.SenderRoleId,
                        principalSchema: "System",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForAllDocument_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalForAllDocument_User_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForAllDocument_AllTypesOfDocumentId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "AllTypesOfDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForAllDocument_ReceiverId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForAllDocument_SenderId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForAllDocument_SenderRoleId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "SenderRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalForAllDocument",
                schema: "GS");
        }
    }
}
