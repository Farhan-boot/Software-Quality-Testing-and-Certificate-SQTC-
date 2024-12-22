using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalForRegistredClientLogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForRegisteredClientLog_PermissionRowSettings_Permis~",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog");

            migrationBuilder.AlterColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalForRegisteredClientLog_PermissionRowSettings_Permis~",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "PermissionRowSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionRowSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForRegisteredClientLog_PermissionRowSettings_Permis~",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog");

            migrationBuilder.AlterColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalForRegisteredClientLog_PermissionRowSettings_Permis~",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "PermissionRowSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionRowSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
