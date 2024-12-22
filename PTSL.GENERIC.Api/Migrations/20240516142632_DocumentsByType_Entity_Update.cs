using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class DocumentsByType_Entity_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentName",
                schema: "GS",
                table: "DocumentsByType",
                newName: "DoumentUniqueName");

            migrationBuilder.AddColumn<string>(
                name: "DocumentTitle",
                schema: "GS",
                table: "DocumentsByType",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentTitle",
                schema: "GS",
                table: "DocumentsByType");

            migrationBuilder.RenameColumn(
                name: "DoumentUniqueName",
                schema: "GS",
                table: "DocumentsByType",
                newName: "DocumentName");
        }
    }
}
