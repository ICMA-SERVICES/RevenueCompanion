using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RevenueCompanion.Infrastructure.Identity.Migrations
{
    public partial class UpdatedIdentityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDisabled",
                schema: "Identity",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnabled",
                schema: "Identity",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "Identity",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "Identity",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabledBy",
                schema: "Identity",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnabledBy",
                schema: "Identity",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MerchantCode",
                schema: "Identity",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDaisbled",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateEnabled",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DisabledBy",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EnabledBy",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MerchantCode",
                schema: "Identity",
                table: "User");
        }
    }
}
