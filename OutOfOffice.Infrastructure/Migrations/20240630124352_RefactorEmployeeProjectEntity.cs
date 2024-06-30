using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEmployeeProjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectId",
                table: "EmployeeProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject");

            migrationBuilder.RenameTable(
                name: "EmployeeProject",
                newName: "EmployeeProjects");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ProjectId",
                table: "EmployeeProjects",
                newName: "IX_EmployeeProjects_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_EmployeeId",
                table: "EmployeeProjects",
                newName: "IX_EmployeeProjects_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployeeId",
                table: "EmployeeProjects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Projects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployeeId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Projects_ProjectId",
                table: "EmployeeProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.RenameTable(
                name: "EmployeeProjects",
                newName: "EmployeeProject");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Projects_ProjectId",
                table: "EmployeeProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
