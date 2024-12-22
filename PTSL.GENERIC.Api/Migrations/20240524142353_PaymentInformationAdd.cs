using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class PaymentInformationAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInformation",
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
                    PaymentCalculationHeaderId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentMethodEnumId = table.Column<int>(type: "integer", nullable: true),
                    FromBankName = table.Column<string>(type: "text", nullable: true),
                    FromBranchName = table.Column<string>(type: "text", nullable: true),
                    FromAddress = table.Column<string>(type: "text", nullable: true),
                    FromAccountNumber = table.Column<string>(type: "text", nullable: true),
                    FromAccountName = table.Column<string>(type: "text", nullable: true),
                    FromDepositDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FromDepositAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    ToBankName = table.Column<string>(type: "text", nullable: true),
                    ToBranchName = table.Column<string>(type: "text", nullable: true),
                    ToAddress = table.Column<string>(type: "text", nullable: true),
                    ToAccountNumber = table.Column<string>(type: "text", nullable: true),
                    ToAccountName = table.Column<string>(type: "text", nullable: true),
                    ChequeNumber = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositName = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ChequeDepositBankName = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositBranchName = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositAddress = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositAccountNumber = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositAccountName = table.Column<string>(type: "text", nullable: true),
                    ChequeDepositAmount = table.Column<double>(type: "double precision", nullable: true),
                    BankDepositorName = table.Column<string>(type: "text", nullable: true),
                    BankDepositeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BankDepositeBankName = table.Column<string>(type: "text", nullable: true),
                    BankDepositeBranchName = table.Column<string>(type: "text", nullable: true),
                    BankDepositeAddress = table.Column<string>(type: "text", nullable: true),
                    BankDepositeAccountNumber = table.Column<string>(type: "text", nullable: true),
                    BankDepositeAccountName = table.Column<string>(type: "text", nullable: true),
                    BankDepositeDepositeAmount = table.Column<double>(type: "double precision", nullable: true),
                    DepositSlipFileId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInformation_PaymentCalculationHeader_PaymentCalculat~",
                        column: x => x.PaymentCalculationHeaderId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "PaymentCalculationHeader",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepositSlipFile",
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
                    PaymentInformationId = table.Column<long>(type: "bigint", nullable: true),
                    FilePathUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositSlipFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositSlipFile_PaymentInformation_PaymentInformationId",
                        column: x => x.PaymentInformationId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "PaymentInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositSlipFile_PaymentInformationId",
                schema: "ProjectPackageConfiguration",
                table: "DepositSlipFile",
                column: "PaymentInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInformation_PaymentCalculationHeaderId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation",
                column: "PaymentCalculationHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositSlipFile",
                schema: "ProjectPackageConfiguration");

            migrationBuilder.DropTable(
                name: "PaymentInformation",
                schema: "ProjectPackageConfiguration");
        }
    }
}
