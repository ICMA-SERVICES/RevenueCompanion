using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class CreatedANewtableCreditNoteRequestType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditNoteRequestTypeId",
                table: "CreditNoteRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreditNoteRequestType",
                columns: table => new
                {
                    CreditNoteRequestTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteRequestType", x => x.CreditNoteRequestTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteRequest_CreditNoteRequestTypeId",
                table: "CreditNoteRequest",
                column: "CreditNoteRequestTypeId");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "CreditNoteRequestType");

            migrationBuilder.DropIndex(
                name: "IX_CreditNoteRequest_CreditNoteRequestTypeId",
                table: "CreditNoteRequest");

            migrationBuilder.DropColumn(
                name: "CreditNoteRequestTypeId",
                table: "CreditNoteRequest");
        }
    }
}
