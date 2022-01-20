using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RevenueCompanion.Infrastructure.Persistence.Migrations
{
    public partial class AddedUserRolePermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersRolePermission",
                columns: table => new
                {
                    UsersRolePermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    MenuSetupId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRolePermission", x => x.UsersRolePermissionId);
                    table.ForeignKey(
                        name: "FK_UsersRolePermission_MenuSetup_MenuSetupId",
                        column: x => x.MenuSetupId,
                        principalTable: "MenuSetup",
                        principalColumn: "MenuSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersRolePermission_MenuSetupId",
                table: "UsersRolePermission",
                column: "MenuSetupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersRolePermission");
        }
    }
}
