using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class BranceTblAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "City",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "HostName",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "PrivateIp",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "PublicIp",
                table: "attendences");

            migrationBuilder.AlterColumn<int>(
                name: "BranceSettingBranceId",
                table: "attendences",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences",
                column: "BranceSettingBranceId",
                principalTable: "branceSettings",
                principalColumn: "BranceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences");

            migrationBuilder.AlterColumn<int>(
                name: "BranceSettingBranceId",
                table: "attendences",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "attendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "attendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "attendences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "attendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateIp",
                table: "attendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicIp",
                table: "attendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences",
                column: "BranceSettingBranceId",
                principalTable: "branceSettings",
                principalColumn: "BranceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
