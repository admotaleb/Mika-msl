using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class tblStudentUpdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "students");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fotter",
                table: "students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sabject",
                table: "students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Fotter",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Sabject",
                table: "students");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
