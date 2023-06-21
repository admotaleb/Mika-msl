using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AttendenceTblUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranceSettingBranceId",
                table: "attendences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "attendences",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "branceSettings",
                columns: table => new
                {
                    BranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branceSettings", x => x.BranceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendences_BranceSettingBranceId",
                table: "attendences",
                column: "BranceSettingBranceId");

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

            migrationBuilder.DropTable(
                name: "branceSettings");

            migrationBuilder.DropIndex(
                name: "IX_attendences_BranceSettingBranceId",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "BranceSettingBranceId",
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
        }
    }
}
