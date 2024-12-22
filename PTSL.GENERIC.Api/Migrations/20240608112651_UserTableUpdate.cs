using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                schema: "System",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "DesignationId",
                schema: "System",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_DesignationId",
                schema: "System",
                table: "User",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Designation_DesignationId",
                schema: "System",
                table: "User",
                column: "DesignationId",
                principalSchema: "GS",
                principalTable: "Designation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Designation_DesignationId",
                schema: "System",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DesignationId",
                schema: "System",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                schema: "System",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                schema: "System",
                table: "User",
                type: "text",
                nullable: true);
        }
    }
}
