using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastDisabledBy",
                table: "AppUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDisabledOn",
                table: "AppUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastEnabledBy",
                table: "AppUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEnabledOn",
                table: "AppUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDisabledBy",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "LastDisabledOn",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "LastEnabledBy",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "LastEnabledOn",
                table: "AppUser");
        }
    }
}
