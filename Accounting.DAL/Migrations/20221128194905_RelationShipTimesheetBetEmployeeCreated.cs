using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class RelationShipTimesheetBetEmployeeCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetEmployeeTimesheet",
                columns: table => new
                {
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesheetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEmployeeTimesheet", x => new { x.EmployeesId, x.TimesheetsId });
                    table.ForeignKey(
                        name: "FK_BetEmployeeTimesheet_BetEmployees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetEmployeeTimesheet_Timesheets_TimesheetsId",
                        column: x => x.TimesheetsId,
                        principalTable: "Timesheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployeeTimesheet_TimesheetsId",
                table: "BetEmployeeTimesheet",
                column: "TimesheetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetEmployeeTimesheet");
        }
    }
}
