using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class userTableUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_User_UserId",
                schema: "GS",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                schema: "GS",
                table: "Client");

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                schema: "System",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                schema: "GS",
                table: "Client",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId",
                schema: "System",
                table: "User",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId1",
                schema: "GS",
                table: "Client",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_User_UserId1",
                schema: "GS",
                table: "Client",
                column: "UserId1",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Client_ClientId",
                schema: "System",
                table: "User",
                column: "ClientId",
                principalSchema: "GS",
                principalTable: "Client",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_User_UserId1",
                schema: "GS",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Client_ClientId",
                schema: "System",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId",
                schema: "System",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId1",
                schema: "GS",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "System",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "GS",
                table: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                schema: "GS",
                table: "Client",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_User_UserId",
                schema: "GS",
                table: "Client",
                column: "UserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
