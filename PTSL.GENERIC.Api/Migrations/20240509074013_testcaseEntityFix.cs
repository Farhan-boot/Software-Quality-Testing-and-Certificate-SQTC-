using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class testcaseEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_User_UserId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropIndex(
                name: "IX_TestCase_UserId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.AlterColumn<long>(
                name: "TaskOfProjectId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectRequestId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_ExecutedByUserId",
                schema: "PR",
                table: "TestCase",
                column: "ExecutedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestCase",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestCase",
                column: "TaskOfProjectId",
                principalSchema: "PR",
                principalTable: "TaskOfProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_User_ExecutedByUserId",
                schema: "PR",
                table: "TestCase",
                column: "ExecutedByUserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCase_User_ExecutedByUserId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.DropIndex(
                name: "IX_TestCase_ExecutedByUserId",
                schema: "PR",
                table: "TestCase");

            migrationBuilder.AlterColumn<long>(
                name: "TaskOfProjectId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectRequestId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "PR",
                table: "TestCase",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_UserId",
                schema: "PR",
                table: "TestCase",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestCase",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestCase",
                column: "TaskOfProjectId",
                principalSchema: "PR",
                principalTable: "TaskOfProject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCase_User_UserId",
                schema: "PR",
                table: "TestCase",
                column: "UserId",
                principalSchema: "System",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
