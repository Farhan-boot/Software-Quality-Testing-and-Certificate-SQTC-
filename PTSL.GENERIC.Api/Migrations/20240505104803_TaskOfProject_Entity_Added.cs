using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PTSL.GENERIC.Api.Migrations
{
    /// <inheritdoc />
    public partial class TaskOfProject_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskOfProject",
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
                    TaskId = table.Column<string>(type: "text", nullable: false),
                    ProjectType = table.Column<int>(type: "integer", nullable: false),
                    ProjectRequestId = table.Column<long>(type: "bigint", nullable: false),
                    TaskTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TaskTitle = table.Column<string>(type: "text", nullable: false),
                    TaskPriority = table.Column<int>(type: "integer", nullable: false),
                    TaskEstimationHour = table.Column<int>(type: "integer", nullable: false),
                    TaskDeadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TaskFileName = table.Column<string>(type: "text", nullable: false),
                    TaskFilePath = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskOfProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskOfProject_ProjectRequest_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalSchema: "PR",
                        principalTable: "ProjectRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskOfProject_TaskType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalSchema: "GS",
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskOfProject_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "System",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskOfProject_ProjectRequestId",
                schema: "PR",
                table: "TaskOfProject",
                column: "ProjectRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskOfProject_TaskTypeId",
                schema: "PR",
                table: "TaskOfProject",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskOfProject_UserId",
                schema: "PR",
                table: "TaskOfProject",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskOfProject",
                schema: "PR");
        }
    }
}
