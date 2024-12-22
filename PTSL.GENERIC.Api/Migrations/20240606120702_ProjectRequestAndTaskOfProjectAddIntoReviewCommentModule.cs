using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectRequestAndTaskOfProjectAddIntoReviewCommentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComment_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComment_TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                column: "TaskOfProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComment_ProjectRequest_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                column: "ProjectRequestId",
                principalSchema: "PR",
                principalTable: "ProjectRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComment_TaskOfProject_TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment",
                column: "TaskOfProjectId",
                principalSchema: "PR",
                principalTable: "TaskOfProject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComment_ProjectRequest_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComment_TaskOfProject_TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComment_ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComment_TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");

            migrationBuilder.DropColumn(
                name: "ProjectRequestId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");

            migrationBuilder.DropColumn(
                name: "TaskOfProjectId",
                schema: "ProjectPackageConfiguration",
                table: "ReviewComment");
        }
    }
}
