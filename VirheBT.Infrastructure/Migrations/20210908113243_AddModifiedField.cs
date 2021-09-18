using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirheBT.Infrastructure.Migrations
{
    public partial class AddModifiedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 8, 13, 32, 43, 4, DateTimeKind.Local).AddTicks(9665));

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ModifiedById",
                table: "Issues",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_ModifiedById",
                table: "Issues",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_ModifiedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ModifiedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Issues");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 6, 11, 13, 1, 25, 113, DateTimeKind.Local).AddTicks(67));
        }
    }
}
