using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class TrainingInstituteAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Institute",
                table: "training",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Employments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Institute",
                table: "training");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Employments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
