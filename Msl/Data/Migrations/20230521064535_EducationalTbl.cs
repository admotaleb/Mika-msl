using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class EducationalTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "educationals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SscExam = table.Column<string>(nullable: true),
                    SscSubject = table.Column<string>(nullable: true),
                    SscInstitute_Name = table.Column<string>(nullable: true),
                    SscPassing_Year = table.Column<string>(nullable: true),
                    SscResult = table.Column<string>(nullable: true),
                    HscExam = table.Column<string>(nullable: true),
                    HscSubject = table.Column<string>(nullable: true),
                    HscInstitute_Name = table.Column<string>(nullable: true),
                    HscPassing_Year = table.Column<string>(nullable: true),
                    HscResult = table.Column<string>(nullable: true),
                    HonorsExam = table.Column<string>(nullable: true),
                    HonorsSubject = table.Column<string>(nullable: true),
                    HonorsInstitute_Name = table.Column<string>(nullable: true),
                    HonorsPassing_Year = table.Column<string>(nullable: true),
                    HonorsResult = table.Column<string>(nullable: true),
                    MastersExam = table.Column<string>(nullable: true),
                    MastersSubject = table.Column<string>(nullable: true),
                    MastersInstitute_Name = table.Column<string>(nullable: true),
                    MastersPassing_Year = table.Column<string>(nullable: true),
                    MastersResult = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educationals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_educationals_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_educationals_ApplicationUserId",
                table: "educationals",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "educationals");
        }
    }
}
