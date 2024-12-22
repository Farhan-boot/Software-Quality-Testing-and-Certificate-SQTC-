using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class DocumentsByType_DBEntity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentsByType",
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
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    MeetingId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentCategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentModuleType = table.Column<int>(type: "integer", nullable: false),
                    DocumentPurpose = table.Column<string>(type: "text", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false),
                    DoumentPath = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsByType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsByType_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "GS",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentsByType_DocumentCategories_DocumentCategoriesId",
                        column: x => x.DocumentCategoriesId,
                        principalSchema: "GS",
                        principalTable: "DocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsByType_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "GS",
                        principalTable: "Meeting",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentsByType_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsByType_ClientId",
                schema: "GS",
                table: "DocumentsByType",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsByType_DocumentCategoriesId",
                schema: "GS",
                table: "DocumentsByType",
                column: "DocumentCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsByType_MeetingId",
                schema: "GS",
                table: "DocumentsByType",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsByType_ProjectRequestId",
                schema: "GS",
                table: "DocumentsByType",
                column: "ProjectRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentsByType",
                schema: "GS");
        }
    }
}
