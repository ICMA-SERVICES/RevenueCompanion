using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.AddColumn<bool>(
                name: "IsGeneral",
                table: "MenuSetup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MerchantConfig",
                columns: table => new
                {
                    MerchantConfigId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    MerchantCode = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BgImage = table.Column<string>(nullable: true),
                    BaseUrl = table.Column<string>(nullable: true),
                    StateFooter = table.Column<string>(maxLength: 250, nullable: true),
                    MerchantWebSite = table.Column<string>(maxLength: 250, nullable: true),
                    MerchantPhone = table.Column<string>(maxLength: 100, nullable: true),
                    MerchantPhone1 = table.Column<string>(maxLength: 100, nullable: true),
                    MerchantEmail = table.Column<string>(maxLength: 100, nullable: true),
                    MerchantAddress = table.Column<string>(maxLength: 350, nullable: true),
                    MerchantAddress2 = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantConfig", x => x.MerchantConfigId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSetting_MenuSetupId",
                table: "ApprovalSetting",
                column: "MenuSetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalSetting_MenuSetup_MenuSetupId",
                table: "ApprovalSetting",
                column: "MenuSetupId",
                principalTable: "MenuSetup",
                principalColumn: "MenuSetupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalSetting_MenuSetup_MenuSetupId",
                table: "ApprovalSetting");

            migrationBuilder.DropTable(
                name: "MerchantConfig");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalSetting_MenuSetupId",
                table: "ApprovalSetting");

            migrationBuilder.DropColumn(
                name: "IsGeneral",
                table: "MenuSetup");

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    MerchantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BgImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.MerchantId);
                });
        }
    }
}
