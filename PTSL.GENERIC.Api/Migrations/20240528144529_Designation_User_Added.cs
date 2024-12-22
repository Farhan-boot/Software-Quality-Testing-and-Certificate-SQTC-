using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class Designation_User_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                schema: "System",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskOfProjectStatus",
                schema: "PR",
                table: "TaskOfProject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                schema: "System",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "TaskOfProjectStatus",
                schema: "PR",
                table: "TaskOfProject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
