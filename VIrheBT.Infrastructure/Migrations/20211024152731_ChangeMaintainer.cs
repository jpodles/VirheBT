using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirheBT.Infrastructure.Migrations
{
    public partial class ChangeMaintainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectMaintainedId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueComments_Issues_IssueId",
                table: "IssueComments");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistories_Issues_IssueId",
                table: "IssueHistories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectMaintainedId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectMaintainedId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "MaintainerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "Created", "MaintainerId" },
                values: new object[] { new DateTime(2021, 10, 24, 17, 27, 31, 228, DateTimeKind.Local).AddTicks(7885), null });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_MaintainerId",
                table: "Projects",
                column: "MaintainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueComments_Issues_IssueId",
                table: "IssueComments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistories_Issues_IssueId",
                table: "IssueHistories",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_MaintainerId",
                table: "Projects",
                column: "MaintainerId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueComments_Issues_IssueId",
                table: "IssueComments");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistories_Issues_IssueId",
                table: "IssueHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_MaintainerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_MaintainerId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "MaintainerId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectMaintainedId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "Created", "MaintainerId" },
                values: new object[] { new DateTime(2021, 9, 8, 13, 32, 43, 4, DateTimeKind.Local).AddTicks(9665), "xxxd" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectMaintainedId",
                table: "AspNetUsers",
                column: "ProjectMaintainedId",
                unique: true,
                filter: "[ProjectMaintainedId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectMaintainedId",
                table: "AspNetUsers",
                column: "ProjectMaintainedId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueComments_Issues_IssueId",
                table: "IssueComments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistories_Issues_IssueId",
                table: "IssueHistories",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
