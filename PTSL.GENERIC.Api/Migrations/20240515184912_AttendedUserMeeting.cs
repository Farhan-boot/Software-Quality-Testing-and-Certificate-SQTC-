using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class AttendedUserMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendedUserMeeting",
                schema: "GS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    SqtcUserId = table.Column<long>(type: "bigint", nullable: false),
                    ClientuserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendedUserMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendedUserMeeting_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "GS",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendedUserMeeting_User_ClientuserId",
                        column: x => x.ClientuserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendedUserMeeting_User_SqtcUserId",
                        column: x => x.SqtcUserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendedUserMeeting_ClientuserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "ClientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendedUserMeeting_MeetingId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendedUserMeeting_SqtcUserId",
                schema: "GS",
                table: "AttendedUserMeeting",
                column: "SqtcUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendedUserMeeting",
                schema: "GS");
        }
    }
}
