using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace VirheBT.Infrastructure.Migrations
{
    public partial class testProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Created", "Description", "MaintainerId", "Name", "Status" },
                values: new object[] { 1, new DateTime(2021, 6, 10, 21, 25, 14, 199, DateTimeKind.Local).AddTicks(8120), "This is test project", "265b5c29-2d5d-4d8b-b68e-6030d124fd14", "TestProject", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1);
        }
    }
}