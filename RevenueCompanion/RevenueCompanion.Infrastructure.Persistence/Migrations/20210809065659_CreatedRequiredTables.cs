using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class CreatedRequiredTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    MerchantCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalSetting",
                columns: table => new
                {
                    ApprovalSettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    MenuSetupId = table.Column<int>(nullable: false),
                    NoOfRequiredApproval = table.Column<int>(nullable: false),
                    MerchantCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalSetting", x => x.ApprovalSettingId);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    AppCode = table.Column<string>(nullable: true),
                    MerchantCode = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteRequest",
                columns: table => new
                {
                    CreditNoteRequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    ApprovalSettingId = table.Column<int>(nullable: false),
                    NoOfRequiredApproval = table.Column<int>(nullable: false),
                    ApprovalCount = table.Column<int>(nullable: false),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    DateApproved = table.Column<DateTime>(nullable: false),
                    ActualAmount = table.Column<double>(nullable: false),
                    AmountUsed = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    AppCode = table.Column<string>(nullable: true),
                    MerchantCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteRequest", x => x.CreditNoteRequestId);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteRequestApprovalDetails",
                columns: table => new
                {
                    CreditNoteRequestApprovalDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    CreditNoteRequestId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    ActedUponBy = table.Column<string>(nullable: true),
                    ApproverUserId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteRequestApprovalDetails", x => x.CreditNoteRequestApprovalDetailId);
                });

            migrationBuilder.CreateTable(
                name: "MenuSetup",
                columns: table => new
                {
                    MenuSetupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    MenuId = table.Column<string>(nullable: true),
                    MenuName = table.Column<string>(nullable: true),
                    ParentMenuId = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    RequiresApproval = table.Column<bool>(nullable: false),
                    AppCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuSetup", x => x.MenuSetupId);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    MerchantId = table.Column<int>(nullable: false)
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
                    BaseUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.MerchantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "ApprovalSetting");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "CreditNoteRequest");

            migrationBuilder.DropTable(
                name: "CreditNoteRequestApprovalDetails");

            migrationBuilder.DropTable(
                name: "MenuSetup");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }
    }
}
