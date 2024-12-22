using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class addProjectRequestIdIntoPaymentInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInformation_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation",
                column: "ProjectRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformation_ProjectRequest_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformation_ProjectRequest_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInformation_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation");

            migrationBuilder.DropColumn(
                name: "ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "PaymentInformation");
        }
    }
}
