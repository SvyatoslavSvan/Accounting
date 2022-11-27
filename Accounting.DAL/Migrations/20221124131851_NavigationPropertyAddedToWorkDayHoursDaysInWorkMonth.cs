using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class NavigationPropertyAddedToWorkDayHoursDaysInWorkMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_HoursDaysInWorkMonths_HoursDaysInWorkMonthId",
                table: "Timesheets");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_HoursDaysInWorkMonthId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "HoursDaysInWorkMonthId",
                table: "Timesheets");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TimesheetId",
                table: "HoursDaysInWorkMonths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HoursDaysInWorkMonths_TimesheetId",
                table: "HoursDaysInWorkMonths",
                column: "TimesheetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDaysInWorkMonths_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonths",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDaysInWorkMonths_Timesheets_TimesheetId",
                table: "HoursDaysInWorkMonths");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_HoursDaysInWorkMonths_TimesheetId",
                table: "HoursDaysInWorkMonths");

            migrationBuilder.DropColumn(
                name: "TimesheetId",
                table: "HoursDaysInWorkMonths");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "HoursDaysInWorkMonthId",
                table: "Timesheets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_HoursDaysInWorkMonthId",
                table: "Timesheets",
                column: "HoursDaysInWorkMonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_HoursDaysInWorkMonths_HoursDaysInWorkMonthId",
                table: "Timesheets",
                column: "HoursDaysInWorkMonthId",
                principalTable: "HoursDaysInWorkMonths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id");
        }
    }
}
