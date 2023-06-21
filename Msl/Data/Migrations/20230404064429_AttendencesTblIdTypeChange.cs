using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AttendencesTblIdTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendences_AspNetUsers_ApplicationUserId1",
                table: "attendences");

            migrationBuilder.DropIndex(
                name: "IX_attendences_ApplicationUserId1",
                table: "attendences");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "attendences");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "attendences",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_attendences_ApplicationUserId",
                table: "attendences",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_AspNetUsers_ApplicationUserId",
                table: "attendences",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendences_AspNetUsers_ApplicationUserId",
                table: "attendences");

            migrationBuilder.DropIndex(
                name: "IX_attendences_ApplicationUserId",
                table: "attendences");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "attendences",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "attendences",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendences_ApplicationUserId1",
                table: "attendences",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_attendences_AspNetUsers_ApplicationUserId1",
                table: "attendences",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
