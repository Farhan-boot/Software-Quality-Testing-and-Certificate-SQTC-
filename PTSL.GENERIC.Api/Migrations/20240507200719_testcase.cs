using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class testcase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCase",
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
                    TestScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    TestCaseNo = table.Column<string>(type: "text", nullable: false),
                    TestCaseName = table.Column<string>(type: "text", nullable: false),
                    CaseDescription = table.Column<string>(type: "text", nullable: false),
                    TestCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ExpectedResult = table.Column<string>(type: "text", nullable: true),
                    ActualResult = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    TestData = table.Column<string>(type: "text", nullable: true),
                    PlannedExecutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActualExecutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Firefox = table.Column<bool>(type: "boolean", nullable: false),
                    Chrome = table.Column<bool>(type: "boolean", nullable: false),
                    IE = table.Column<bool>(type: "boolean", nullable: false),
                    Edge = table.Column<bool>(type: "boolean", nullable: false),
                    TestResult = table.Column<int>(type: "integer", nullable: false),
                    ExecutedByUserId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCase_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestCase_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestCase_TestCategory_TestCategoryId",
                        column: x => x.TestCategoryId,
                        principalSchema: "PR",
                        principalTable: "TestCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCase_TestScenario_TestScenarioId",
                        column: x => x.TestScenarioId,
                        principalSchema: "PR",
                        principalTable: "TestScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCase_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_ProjectRequestId",
                schema: "PR",
                table: "TestCase",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_TaskOfProjectId",
                schema: "PR",
                table: "TestCase",
                column: "TaskOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_TestCategoryId",
                schema: "PR",
                table: "TestCase",
                column: "TestCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_TestScenarioId",
                schema: "PR",
                table: "TestCase",
                column: "TestScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_UserId",
                schema: "PR",
                table: "TestCase",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCase",
                schema: "PR");
        }
    }
}
