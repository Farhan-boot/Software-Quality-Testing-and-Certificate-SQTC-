using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ReconciliationAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reconciliation",
                schema: "ProjectPackageConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentInformationId = table.Column<long>(type: "bigint", nullable: false),
                    IsPaymentDisbursement = table.Column<bool>(type: "boolean", nullable: true),
                    DisbursementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DisbursementAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    IsPaymentRelease = table.Column<bool>(type: "boolean", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReleaseAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    ReconciliationRemark = table.Column<string>(type: "text", nullable: true),
                    IsPaymentApproved = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconciliation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reconciliation_PaymentInformation_PaymentInformationId",
                        column: x => x.PaymentInformationId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "PaymentInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reconciliation_PaymentInformationId",
                schema: "ProjectPackageConfiguration",
                table: "Reconciliation",
                column: "PaymentInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reconciliation",
                schema: "ProjectPackageConfiguration");
        }
    }
}
