using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class permissionHeadersettingupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionHeaderSettings_Accesslist_AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings");

            migrationBuilder.DropIndex(
                name: "IX_PermissionHeaderSettings_AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings");

            migrationBuilder.DropColumn(
                name: "AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings");

            migrationBuilder.AddColumn<long>(
                name: "PermissionHeaderSettingsId",
                schema: "System",
                table: "Accesslist",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accesslist_PermissionHeaderSettingsId",
                schema: "System",
                table: "Accesslist",
                column: "PermissionHeaderSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accesslist_PermissionHeaderSettings_PermissionHeaderSetting~",
                schema: "System",
                table: "Accesslist",
                column: "PermissionHeaderSettingsId",
                principalSchema: "PermissionSettings",
                principalTable: "PermissionHeaderSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accesslist_PermissionHeaderSettings_PermissionHeaderSetting~",
                schema: "System",
                table: "Accesslist");

            migrationBuilder.DropIndex(
                name: "IX_Accesslist_PermissionHeaderSettingsId",
                schema: "System",
                table: "Accesslist");

            migrationBuilder.DropColumn(
                name: "PermissionHeaderSettingsId",
                schema: "System",
                table: "Accesslist");

            migrationBuilder.AddColumn<long>(
                name: "AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionHeaderSettings_AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                column: "AccesslistId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionHeaderSettings_Accesslist_AccesslistId",
                schema: "PermissionSettings",
                table: "PermissionHeaderSettings",
                column: "AccesslistId",
                principalSchema: "System",
                principalTable: "Accesslist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
