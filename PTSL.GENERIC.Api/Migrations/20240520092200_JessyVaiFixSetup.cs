using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class JessyVaiFixSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ProjectPackageConfiguration");

            migrationBuilder.CreateTable(
                name: "PaymentCalculationHeader",
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
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: true),
                    TaskOfProjectId = table.Column<long>(type: "bigint", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    GrandTotalInWord = table.Column<string>(type: "text", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    NetTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCalculationHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCalculationHeader_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCalculationHeader_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectModuleName",
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProjectTypeId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModuleName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackage",
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
                    ProjectModuleNameId = table.Column<long>(type: "bigint", nullable: false),
                    PackageName = table.Column<string>(type: "varchar(500)", nullable: true),
                    PackageDescription = table.Column<string>(type: "varchar(500)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackage_ProjectModuleName_ProjectModuleNameId",
                        column: x => x.ProjectModuleNameId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "ProjectModuleName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCalculationRow",
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
                    PaymentCalculationHeaderId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectModuleNameId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectPackageId = table.Column<long>(type: "bigint", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    NumberOfPackage = table.Column<long>(type: "bigint", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Vat = table.Column<decimal>(type: "numeric", nullable: true),
                    Tax = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCalculationRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCalculationRow_PaymentCalculationHeader_PaymentCalcu~",
                        column: x => x.PaymentCalculationHeaderId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "PaymentCalculationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentCalculationRow_ProjectModuleName_ProjectModuleNameId",
                        column: x => x.ProjectModuleNameId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "ProjectModuleName",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCalculationRow_ProjectPackage_ProjectPackageId",
                        column: x => x.ProjectPackageId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "ProjectPackage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPricingSetup",
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
                    ProjectModuleNameId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectPackageId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPricingSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPricingSetup_ProjectModuleName_ProjectModuleNameId",
                        column: x => x.ProjectModuleNameId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "ProjectModuleName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPricingSetup_ProjectPackage_ProjectPackageId",
                        column: x => x.ProjectPackageId,
                        principalSchema: "ProjectPackageConfiguration",
                        principalTable: "ProjectPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCalculationHeader_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentCalculationHeader",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCalculationHeader_TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentCalculationHeader",
                column: "TaskOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCalculationRow_PaymentCalculationHeaderId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentCalculationRow",
                column: "PaymentCalculationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCalculationRow_ProjectModuleNameId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentCalculationRow",
                column: "ProjectModuleNameId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCalculationRow_ProjectPackageId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentCalculationRow",
                column: "ProjectPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackage_ProjectModuleNameId",
                schema: "ProjectPackageConfiguration",
                table: "ProjectPackage",
                column: "ProjectModuleNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPricingSetup_ProjectModuleNameId",
                schema: "ProjectPackageConfiguration",
                table: "ProjectPricingSetup",
                column: "ProjectModuleNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPricingSetup_ProjectPackageId",
                schema: "ProjectPackageConfiguration",
                table: "ProjectPricingSetup",
                column: "ProjectPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentCalculationRow",
                schema: "ProjectPackageConfiguration");

            migrationBuilder.DropTable(
                name: "ProjectPricingSetup",
                schema: "ProjectPackageConfiguration");

            migrationBuilder.DropTable(
                name: "PaymentCalculationHeader",
                schema: "ProjectPackageConfiguration");

            migrationBuilder.DropTable(
                name: "ProjectPackage",
                schema: "ProjectPackageConfiguration");

            migrationBuilder.DropTable(
                name: "ProjectModuleName",
                schema: "ProjectPackageConfiguration");
        }
    }
}
