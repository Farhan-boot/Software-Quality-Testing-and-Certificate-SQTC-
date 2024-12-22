using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class meetingattendedUerNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendedUserMeeting_User_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendedUserMeeting_User_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "AttendeduserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendedUserMeeting_User_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendedUserMeeting_User_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "AttendeduserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
