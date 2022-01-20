using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedMerchantCodeToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppCode",
                table: "UsersRolePermission");

            migrationBuilder.DropColumn(
                name: "AppCode",
                table: "MenuSetup");

            migrationBuilder.DropColumn(
                name: "AppCode",
                table: "CreditNoteRequest");

            migrationBuilder.AddColumn<string>(
                name: "MerchantCode",
                table: "UsersRolePermission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantCode",
                table: "MenuSetup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantCode",
                table: "CreditNoteRequestApprovalDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MerchantCode",
                table: "UsersRolePermission");

            migrationBuilder.DropColumn(
                name: "MerchantCode",
                table: "MenuSetup");

            migrationBuilder.DropColumn(
                name: "MerchantCode",
                table: "CreditNoteRequestApprovalDetails");

            migrationBuilder.AddColumn<string>(
                name: "AppCode",
                table: "UsersRolePermission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppCode",
                table: "MenuSetup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppCode",
                table: "CreditNoteRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
