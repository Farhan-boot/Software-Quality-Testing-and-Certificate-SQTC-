using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class TaskScenario_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestScenario",
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
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: false),
                    TaskOfProjectId = table.Column<long>(type: "bigint", nullable: false),
                    TestScenarioNo = table.Column<string>(type: "text", nullable: false),
                    Module = table.Column<string>(type: "text", nullable: false),
                    SubModule1 = table.Column<string>(type: "text", nullable: false),
                    SubModule2 = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    ScenarioDescription = table.Column<string>(type: "text", nullable: false),
                    TC = table.Column<int>(type: "integer", nullable: false),
                    POC = table.Column<string>(type: "text", nullable: true),
                    TaskPriority = table.Column<int>(type: "integer", nullable: false),
                    PlannedExecutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActualExecutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestScenario_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestScenario_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestScenario_ProjectRequestId",
                schema: "PR",
                table: "TestScenario",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScenario_TaskOfProjectId",
                schema: "PR",
                table: "TestScenario",
                column: "TaskOfProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestScenario",
                schema: "PR");
        }
    }
}
