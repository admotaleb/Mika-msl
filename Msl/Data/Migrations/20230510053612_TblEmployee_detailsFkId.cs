using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class TblEmployee_detailsFkId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserUserName",
                table: "employee_Details");

            migrationBuilder.DropIndex(
                name: "IX_employee_Details_ApplicationUserUserName",
                table: "employee_Details");

            migrationBuilder.DropColumn(
                name: "ApplicationUserUserName",
                table: "employee_Details");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "employee_Details",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_Details_ApplicationUserId",
                table: "employee_Details",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserId",
                table: "employee_Details",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserId",
                table: "employee_Details");

            migrationBuilder.DropIndex(
                name: "IX_employee_Details_ApplicationUserId",
                table: "employee_Details");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "employee_Details");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserUserName",
                table: "employee_Details",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_Details_ApplicationUserUserName",
                table: "employee_Details",
                column: "ApplicationUserUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_Details_AspNetUsers_ApplicationUserUserName",
                table: "employee_Details",
                column: "ApplicationUserUserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
