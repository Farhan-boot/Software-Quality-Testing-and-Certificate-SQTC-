using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class BugAndDefect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugAndDefect",
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
                    TestCaseId = table.Column<long>(type: "bigint", nullable: false),
                    DefectId = table.Column<long>(type: "bigint", nullable: false),
                    BugzillaId = table.Column<string>(type: "text", nullable: false),
                    Component = table.Column<string>(type: "text", nullable: false),
                    BugAndDefectSeverity = table.Column<int>(type: "integer", nullable: false),
                    BugAndDefectStatus = table.Column<int>(type: "integer", nullable: false),
                    ExpectedResult = table.Column<string>(type: "text", nullable: true),
                    ActualResult = table.Column<string>(type: "text", nullable: true),
                    Resulation = table.Column<string>(type: "text", nullable: true),
                    DefectedSummary = table.Column<string>(type: "text", nullable: true),
                    StepstoReproduce = table.Column<string>(type: "text", nullable: true),
                    ReportedBy = table.Column<long>(type: "bigint", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugAndDefect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugAndDefect_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugAndDefect_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugAndDefect_TestCase_TestCaseId",
                        column: x => x.TestCaseId,
                        principalSchema: "PR",
                        principalTable: "TestCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugAndDefect_User_ReportedBy",
                        column: x => x.ReportedBy,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BugAndDefectFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    BugAndDefectId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileNameUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugAndDefectFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugAndDefectFile_BugAndDefect_BugAndDefectId",
                        column: x => x.BugAndDefectId,
                        principalSchema: "PR",
                        principalTable: "BugAndDefect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BugAndDefect_ProjectRequestId",
                schema: "PR",
                table: "BugAndDefect",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BugAndDefect_ReportedBy",
                schema: "PR",
                table: "BugAndDefect",
                column: "ReportedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BugAndDefect_TaskOfProjectId",
                schema: "PR",
                table: "BugAndDefect",
                column: "TaskOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BugAndDefect_TestCaseId",
                schema: "PR",
                table: "BugAndDefect",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BugAndDefectFile_BugAndDefectId",
                table: "BugAndDefectFile",
                column: "BugAndDefectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugAndDefectFile");

            migrationBuilder.DropTable(
                name: "BugAndDefect",
                schema: "PR");
        }
    }
}
