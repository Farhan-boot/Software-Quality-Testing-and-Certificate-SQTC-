using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class teststepModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestStep_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.DropForeignKey(
                name: "FK_TestStep_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.AlterColumn<long>(
                name: "TaskOfProjectId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectRequestId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestStep_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestStep",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestStep_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestStep",
                column: "TaskOfProjectId",
                principalSchema: "PR",
                principalTable: "TaskOfProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestStep_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.DropForeignKey(
                name: "FK_TestStep_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestStep");

            migrationBuilder.AlterColumn<long>(
                name: "TaskOfProjectId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectRequestId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                schema: "PR",
                table: "TestStep",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_TestStep_ProjectRequest_ProjectRequestId",
                schema: "PR",
                table: "TestStep",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestStep_TaskOfProject_TaskOfProjectId",
                schema: "PR",
                table: "TestStep",
                column: "TaskOfProjectId",
                principalSchema: "PR",
                principalTable: "TaskOfProject",
                principalColumn: "Id");
        }
    }
}
