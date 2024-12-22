using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class projectApprovalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectApprovalStatus",
                schema: "PR",
                table: "ProjectRequest",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TestStep",
                schema: "PR",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: true),
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    TaskOfProjectId = table.Column<long>(type: "bigint", nullable: true),
                    TestCaseId = table.Column<long>(type: "bigint", nullable: false),
                    Test_Step = table.Column<string>(type: "text", nullable: false),
                    TestData = table.Column<string>(type: "text", nullable: false),
                    ExpectedResult = table.Column<string>(type: "text", nullable: true),
                    ActualResult = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestStep_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestStep_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestStep_TestCase_TestCaseId",
                        column: x => x.TestCaseId,
                        principalSchema: "PR",
                        principalTable: "TestCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestStep_ProjectRequestId",
                schema: "PR",
                table: "TestStep",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStep_TaskOfProjectId",
                schema: "PR",
                table: "TestStep",
                column: "TaskOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStep_TestCaseId",
                schema: "PR",
                table: "TestStep",
                column: "TestCaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestStep",
                schema: "PR");

            migrationBuilder.DropColumn(
                name: "ProjectApprovalStatus",
                schema: "PR",
                table: "ProjectRequest");
        }
    }
}
