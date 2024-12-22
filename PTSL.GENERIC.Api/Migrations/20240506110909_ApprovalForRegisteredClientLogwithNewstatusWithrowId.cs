using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalForRegisteredClientLogwithNewstatusWithrowId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalForRegisteredClientLog_PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog",
                column: "PermissionRowSettingsId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalForRegisteredClientLog_PermissionRowSettings_Permis~",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalForRegisteredClientLog_PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog");

            migrationBuilder.DropColumn(
                name: "PermissionRowSettingsId",
                schema: "GS",
                table: "ApprovalForRegisteredClientLog");
        }
    }
}
