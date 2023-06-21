using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class TblDepartmentFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee_Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fathers_Name = table.Column<string>(nullable: true),
                    Mothers_Name = table.Column<string>(nullable: true),
                    Spouse_Name = table.Column<string>(nullable: true),
                    Desgnation = table.Column<string>(nullable: true),
                    DseAuthorized = table.Column<string>(nullable: true),
                    Joning_Date = table.Column<string>(nullable: true),
                    Blood_Group = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    Contact_No_Personal = table.Column<string>(nullable: true),
                    EmergencyContact = table.Column<string>(nullable: true),
                    Present_Address = table.Column<string>(nullable: true),
                    Permanent_Address = table.Column<string>(nullable: true),
                    Pictures = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_Details_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_Details_DepartmentId",
                table: "employee_Details",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_Details");
        }
    }
}
