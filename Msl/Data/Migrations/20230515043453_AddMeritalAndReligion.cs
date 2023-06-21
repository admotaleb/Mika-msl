using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AddMeritalAndReligion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "employee_Details",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "employee_Details",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "employee_Details");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "employee_Details");
        }
    }
}
