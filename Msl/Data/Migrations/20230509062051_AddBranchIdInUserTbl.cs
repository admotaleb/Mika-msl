using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AddBranchIdInUserTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BranceSettingBranceId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BranceSettingBranceId",
                table: "AspNetUsers",
                column: "BranceSettingBranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_branceSettings_BranceSettingBranceId",
                table: "AspNetUsers",
                column: "BranceSettingBranceId",
                principalTable: "branceSettings",
                principalColumn: "BranceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_branceSettings_BranceSettingBranceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BranceSettingBranceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BranceSettingBranceId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
