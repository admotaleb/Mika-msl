using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AttendenceLatitudeDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences");

            migrationBuilder.AlterColumn<int>(
                name: "BranceSettingBranceId",
                table: "attendences",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "attendences",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateIp",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicIp",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences",
                column: "BranceSettingBranceId",
                principalTable: "branceSettings",
                principalColumn: "BranceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_branceSettings_BranceSettingBranceId",
                table: "attendences",
                column: "BranceSettingBranceId",
                principalTable: "branceSettings",
                principalColumn: "BranceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
