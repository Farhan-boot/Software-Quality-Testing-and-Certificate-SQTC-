using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class newMeetingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInitiatedBySqtc",
                schema: "GS",
                table: "Meeting",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingStatus",
                schema: "GS",
                table: "Meeting",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInitiatedBySqtc",
                schema: "GS",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "MeetingStatus",
                schema: "GS",
                table: "Meeting");
        }
    }
}
