using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalDocument_Entity_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForAllDocument_PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "PermissionRowSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalForAllDocument_PermissionRowSettings_PermissionRowS~",
                schema: "GS",
                table: "ApprovalForAllDocument",
                column: "PermissionRowSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionRowSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForAllDocument_PermissionRowSettings_PermissionRowS~",
                schema: "GS",
                table: "ApprovalForAllDocument");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalForAllDocument_PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForAllDocument");

            migrationBuilder.DropColumn(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForAllDocument");
        }
    }
}
