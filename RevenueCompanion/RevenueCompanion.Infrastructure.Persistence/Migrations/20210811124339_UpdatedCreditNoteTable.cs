using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedCreditNoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppCode",
                table: "UsersRolePermission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedByEmail",
                table: "CreditNoteRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedById",
                table: "CreditNoteRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedByName",
                table: "CreditNoteRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteRequestApprovalDetails_CreditNoteRequestId",
                table: "CreditNoteRequestApprovalDetails",
                column: "CreditNoteRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditNoteRequestApprovalDetails_CreditNoteRequest_CreditNoteRequestId",
                table: "CreditNoteRequestApprovalDetails",
                column: "CreditNoteRequestId",
                principalTable: "CreditNoteRequest",
                principalColumn: "CreditNoteRequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditNoteRequestApprovalDetails_CreditNoteRequest_CreditNoteRequestId",
                table: "CreditNoteRequestApprovalDetails");

            migrationBuilder.DropIndex(
                name: "IX_CreditNoteRequestApprovalDetails_CreditNoteRequestId",
                table: "CreditNoteRequestApprovalDetails");

            migrationBuilder.DropColumn(
                name: "AppCode",
                table: "UsersRolePermission");

            migrationBuilder.DropColumn(
                name: "RequestedByEmail",
                table: "CreditNoteRequest");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "CreditNoteRequest");

            migrationBuilder.DropColumn(
                name: "RequestedByName",
                table: "CreditNoteRequest");
        }
    }
}
