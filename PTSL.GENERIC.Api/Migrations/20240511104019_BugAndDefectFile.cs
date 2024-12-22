using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class BugAndDefectFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugAndDefectFile_BugAndDefect_BugAndDefectId",
                table: "BugAndDefectFile");

            migrationBuilder.RenameTable(
                name: "BugAndDefectFile",
                newName: "BugAndDefectFile",
                newSchema: "PR");

            migrationBuilder.AddForeignKey(
                name: "FK_BugAndDefectFile_BugAndDefect_BugAndDefectId",
                schema: "PR",
                table: "BugAndDefectFile",
                column: "BugAndDefectId",
                principalSchema: "PR",
                principalTable: "BugAndDefect",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugAndDefectFile_BugAndDefect_BugAndDefectId",
                schema: "PR",
                table: "BugAndDefectFile");

            migrationBuilder.RenameTable(
                name: "BugAndDefectFile",
                schema: "PR",
                newName: "BugAndDefectFile");

            migrationBuilder.AddForeignKey(
                name: "FK_BugAndDefectFile_BugAndDefect_BugAndDefectId",
                table: "BugAndDefectFile",
                column: "BugAndDefectId",
                principalSchema: "PR",
                principalTable: "BugAndDefect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
