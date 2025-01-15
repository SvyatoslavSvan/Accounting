using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityTimesheetHoursDaysInWorkMonthCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TimesheetId",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HoursDaysInWorkMonths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoursCount = table.Column<int>(type: "int", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursDaysInWorkMonths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoursDaysInWorkMonthId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_HoursDaysInWorkMonths_HoursDaysInWorkMonthId",
                        column: x => x.HoursDaysInWorkMonthId,
                        principalTable: "HoursDaysInWorkMonths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_TimesheetId",
                table: "WorkDays",
                column: "TimesheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_HoursDaysInWorkMonthId",
                table: "Timesheets",
                column: "HoursDaysInWorkMonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "HoursDaysInWorkMonths");

            migrationBuilder.DropIndex(
                name: "IX_WorkDays_TimesheetId",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "TimesheetId",
                table: "WorkDays");
        }
    }
}
