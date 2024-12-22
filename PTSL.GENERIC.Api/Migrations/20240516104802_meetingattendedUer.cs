using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class meetingattendedUer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendedUserMeeting_User_ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendedUserMeeting_User_SqtcUserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.DropIndex(
                name: "IX_AttendedUserMeeting_ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.DropColumn(
                name: "ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.RenameColumn(
                name: "SqtcUserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                newName: "AttendeduserId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendedUserMeeting_SqtcUserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                newName: "IX_AttendedUserMeeting_AttendeduserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendedUserMeeting_User_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting");

            migrationBuilder.RenameColumn(
                name: "AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                newName: "SqtcUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendedUserMeeting_AttendeduserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                newName: "IX_AttendedUserMeeting_SqtcUserId");

            migrationBuilder.AddColumn<long>(
                name: "ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AttendedUserMeeting_ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "ClientuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendedUserMeeting_User_ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "ClientuserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendedUserMeeting_User_SqtcUserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "SqtcUserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
