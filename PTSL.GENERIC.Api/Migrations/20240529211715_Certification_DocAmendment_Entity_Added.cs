using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class Certification_DocAmendment_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAmendment",
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
                    AmendmentComment = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAmendment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAmendment_AllTypesOfDocument_AllTypesOfDocumentId",
                        column: x => x.AllTypesOfDocumentId,
                        principalSchema: "GS",
                        principalTable: "AllTypesOfDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCertification",
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
                    CertificateFilePath = table.Column<string>(type: "text", nullable: true),
                    CertificateFileName = table.Column<string>(type: "text", nullable: true),
                    CertificateHashNo = table.Column<string>(type: "text", nullable: true),
                    CertificateContent = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCertification_AllTypesOfDocument_AllTypesOfDocumentId",
                        column: x => x.AllTypesOfDocumentId,
                        principalSchema: "GS",
                        principalTable: "AllTypesOfDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAmendment_AllTypesOfDocumentId",
                schema: "GS",
                table: "DocumentAmendment",
                column: "AllTypesOfDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCertification_AllTypesOfDocumentId",
                schema: "GS",
                table: "ProjectCertification",
                column: "AllTypesOfDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAmendment",
                schema: "GS");

            migrationBuilder.DropTable(
                name: "ProjectCertification",
                schema: "GS");
        }
    }
}
