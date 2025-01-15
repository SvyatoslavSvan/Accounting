using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class HoursCountDaysCountAddedToTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoursDaysInWorkMonth");

            migrationBuilder.AddColumn<int>(
                name: "DaysCount",
                table: "Timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "HoursCount",
                table: "Timesheets",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysCount",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "HoursCount",
                table: "Timesheets");

            migrationBuilder.CreateTable(
                name: "HoursDaysInWorkMonth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false),
                    HoursCount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursDaysInWorkMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoursDaysInWorkMonth_Timesheets_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "Timesheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoursDaysInWorkMonth_TimesheetId",
                table: "HoursDaysInWorkMonth",
                column: "TimesheetId",
                unique: true);
        }
    }
}
