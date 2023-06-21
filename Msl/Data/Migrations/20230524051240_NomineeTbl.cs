using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class NomineeTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nominees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomineeName = table.Column<string>(nullable: true),
                    LegalGuardianName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Relation = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Entitlement = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nominees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nominees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nominees_ApplicationUserId",
                table: "nominees",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nominees");
        }
    }
}
