using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorApprovalRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                unique: true,
                filter: "[ApproverId] IS NOT NULL");
        }
    }
}
