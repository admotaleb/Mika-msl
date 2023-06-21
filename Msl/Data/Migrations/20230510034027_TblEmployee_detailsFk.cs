using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class TblEmployee_detailsFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "employee_Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "employee_Details",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_Details_ApplicationUserId1",
                table: "employee_Details",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserId1",
                table: "employee_Details",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserId1",
                table: "employee_Details");

            migrationBuilder.DropIndex(
                name: "IX_employee_Details_ApplicationUserId1",
                table: "employee_Details");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "employee_Details");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "employee_Details");
        }
    }
}
