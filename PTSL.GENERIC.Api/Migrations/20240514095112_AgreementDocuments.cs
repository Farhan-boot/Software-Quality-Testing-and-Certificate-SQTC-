using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgreementDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementDocuments",
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
                    Agreement_FirstPage = table.Column<string>(type: "text", nullable: false),
                    Agreement_SecondPage = table.Column<string>(type: "text", nullable: false),
                    Agreement_ThirdPage = table.Column<string>(type: "text", nullable: false),
                    Agreement_ForthPage = table.Column<string>(type: "text", nullable: false),
                    Agreement_LastPage = table.Column<string>(type: "text", nullable: false),
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: true),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreementDocuments_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "GS",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgreementDocuments_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDocuments_ClientId",
                schema: "GS",
                table: "AgreementDocuments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDocuments_ProjectRequestId",
                schema: "GS",
                table: "AgreementDocuments",
                column: "ProjectRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementDocuments",
                schema: "GS");
        }
    }
}
