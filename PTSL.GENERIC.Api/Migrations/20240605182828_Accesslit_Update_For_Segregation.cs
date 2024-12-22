using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class Accesslit_Update_For_Segregation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBothForSqtcAndClient",
                schema: "System",
                table: "Accesslist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForClient",
                schema: "System",
                table: "Accesslist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForSqtc",
                schema: "System",
                table: "Accesslist",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBothForSqtcAndClient",
                schema: "System",
                table: "Accesslist");

            migrationBuilder.DropColumn(
                name: "IsForClient",
                schema: "System",
                table: "Accesslist");

            migrationBuilder.DropColumn(
                name: "IsForSqtc",
                schema: "System",
                table: "Accesslist");
        }
    }
}
