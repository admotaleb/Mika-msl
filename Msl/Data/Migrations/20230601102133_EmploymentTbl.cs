using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class EmploymentTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Employments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employments_ApplicationUserId",
                table: "Employments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employments_AspNetUsers_ApplicationUserId",
                table: "Employments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employments_AspNetUsers_ApplicationUserId",
                table: "Employments");

            migrationBuilder.DropIndex(
                name: "IX_Employments_ApplicationUserId",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Employments");
        }
    }
}
