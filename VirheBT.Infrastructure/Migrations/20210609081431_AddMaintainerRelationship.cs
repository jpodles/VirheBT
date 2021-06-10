using Microsoft.EntityFrameworkCore.Migrations;

namespace VirheBT.Infrastructure.Migrations
{
    public partial class AddMaintainerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaintainerId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectMaintainedId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectMaintainedId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectMaintainedId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaintainerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectMaintainedId",
                table: "AspNetUsers");
        }
    }
}
