using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Msl.Data.Migrations
{
    public partial class ActivitesReportTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivitiesReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trader = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Turnover = table.Column<double>(nullable: false),
                    AverageClients = table.Column<int>(nullable: false),
                    BoOpen = table.Column<int>(nullable: false),
                    Investment = table.Column<double>(nullable: false),
                    Withdraw = table.Column<double>(nullable: false),
                    Visit = table.Column<int>(nullable: false),
                    ClientsNo = table.Column<string>(nullable: true),
                    ExpectedBoOpen = table.Column<int>(nullable: false),
                    ZoomMeeting = table.Column<int>(nullable: false),
                    PhysicalVisit = table.Column<int>(nullable: false),
                    Leave = table.Column<int>(nullable: false),
                    BranceSettingBranceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitiesReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitiesReports_branceSettings_BranceSettingBranceId",
                        column: x => x.BranceSettingBranceId,
                        principalTable: "branceSettings",
                        principalColumn: "BranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesReports_BranceSettingBranceId",
                table: "ActivitiesReports",
                column: "BranceSettingBranceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitiesReports");
        }
    }
}
