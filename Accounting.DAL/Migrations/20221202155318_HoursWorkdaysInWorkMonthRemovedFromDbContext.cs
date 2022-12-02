using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class HoursWorkdaysInWorkMonthRemovedFromDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDaysInWorkMonths_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoursDaysInWorkMonths",
                table: "HoursDaysInWorkMonths");

            migrationBuilder.RenameTable(
                name: "HoursDaysInWorkMonths",
                newName: "HoursDaysInWorkMonth");

            migrationBuilder.RenameIndex(
                name: "IX_HoursDaysInWorkMonths_TimesheetId",
                table: "HoursDaysInWorkMonth",
                newName: "IX_HoursDaysInWorkMonth_TimesheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoursDaysInWorkMonth",
                table: "HoursDaysInWorkMonth",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDaysInWorkMonth_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonth",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDaysInWorkMonth_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoursDaysInWorkMonth",
                table: "HoursDaysInWorkMonth");

            migrationBuilder.RenameTable(
                name: "HoursDaysInWorkMonth",
                newName: "HoursDaysInWorkMonths");

            migrationBuilder.RenameIndex(
                name: "IX_HoursDaysInWorkMonth_TimesheetId",
                table: "HoursDaysInWorkMonths",
                newName: "IX_HoursDaysInWorkMonths_TimesheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoursDaysInWorkMonths",
                table: "HoursDaysInWorkMonths",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDaysInWorkMonths_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonths",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
