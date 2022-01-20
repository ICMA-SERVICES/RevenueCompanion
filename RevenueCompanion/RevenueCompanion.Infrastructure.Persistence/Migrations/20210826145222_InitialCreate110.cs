using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "CreditNoteRequest");

            migrationBuilder.AddColumn<string>(
                name: "AssessmentReferenceNumber",
                table: "CreditNoteRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentReferenceNumber",
                table: "CreditNoteRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentReferenceNumber",
                table: "CreditNoteRequest");

            migrationBuilder.DropColumn(
                name: "PaymentReferenceNumber",
                table: "CreditNoteRequest");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "CreditNoteRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
