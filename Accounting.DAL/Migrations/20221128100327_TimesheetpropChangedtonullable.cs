using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class TimesheetpropChangedtonullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Timesheets_TimesheetId",
                table: "WorkDays",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
