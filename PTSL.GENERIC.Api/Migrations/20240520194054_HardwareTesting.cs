using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class HardwareTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardwareTesting",
                schema: "HT",
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
                    TestScopeId = table.Column<long>(type: "bigint", nullable: false),
                    SubItem = table.Column<string>(type: "text", nullable: false),
                    RequiredSpecification = table.Column<string>(type: "text", nullable: false),
                    SpecificationAsPerContract = table.Column<string>(type: "text", nullable: true),
                    TestResult = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareTesting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareTesting_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HardwareTesting_TaskOfProject_TaskOfProjectId",
                        column: x => x.TaskOfProjectId,
                        principalSchema: "PR",
                        principalTable: "TaskOfProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HardwareTesting_TestScope_TestScopeId",
                        column: x => x.TestScopeId,
                        principalSchema: "HT",
                        principalTable: "TestScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareTesting_ProjectRequestId",
                schema: "HT",
                table: "HardwareTesting",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareTesting_TaskOfProjectId",
                schema: "HT",
                table: "HardwareTesting",
                column: "TaskOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareTesting_TestScopeId",
                schema: "HT",
                table: "HardwareTesting",
                column: "TestScopeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareTesting",
                schema: "HT");
        }
    }
}
