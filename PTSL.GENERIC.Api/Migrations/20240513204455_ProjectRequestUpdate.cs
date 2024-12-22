using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectRequestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForProjectLog_PermissionRowSettings_PermissionRowSe~",
                schema: "GS",
                table: "ApprovalForProjectLog");

            migrationBuilder.AlterColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalForProjectLog_PermissionRowSettings_PermissionRowSe~",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "PermissionRowSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionRowSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForProjectLog_PermissionRowSettings_PermissionRowSe~",
                schema: "GS",
                table: "ApprovalForProjectLog");

            migrationBuilder.AlterColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForProjectLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalForProjectLog_PermissionRowSettings_PermissionRowSe~",
                schema: "GS",
                table: "ApprovalForProjectLog",
                column: "PermissionRowSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionRowSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
