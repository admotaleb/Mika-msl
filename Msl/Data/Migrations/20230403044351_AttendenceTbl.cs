using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class AttendenceTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attendences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<string>(nullable: true),
                    CheckInReason = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    CheckOut = table.Column<string>(nullable: true),
                    CheckOutReason = table.Column<string>(nullable: true),
                    Present = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendences_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "timeSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InTime = table.Column<string>(nullable: true),
                    OutTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeSetting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendences_ApplicationUserId1",
                table: "attendences",
                column: "ApplicationUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendences");

            migrationBuilder.DropTable(
                name: "timeSetting");
        }
    }
}
