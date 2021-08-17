using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace VirheBT.Infrastructure.Migrations
{
    public partial class AddUserRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "Created", "MaintainerId" },
                values: new object[] { new DateTime(2021, 6, 11, 13, 1, 25, 113, DateTimeKind.Local).AddTicks(67), "xxxd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "Created", "MaintainerId" },
                values: new object[] { new DateTime(2021, 6, 10, 21, 25, 14, 199, DateTimeKind.Local).AddTicks(8120), "265b5c29-2d5d-4d8b-b68e-6030d124fd14" });
        }
    }
}