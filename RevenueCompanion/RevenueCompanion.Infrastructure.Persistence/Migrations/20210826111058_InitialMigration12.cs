using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppName",
                table: "MerchantConfig",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppName",
                table: "MerchantConfig");
        }
    }
}
