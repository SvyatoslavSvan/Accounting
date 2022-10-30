using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class PropBetEmployeeInDeducationChangedToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_BetEmployees_BetEmployeeId",
                table: "Deducations");

            migrationBuilder.AlterColumn<Guid>(
                name: "BetEmployeeId",
                table: "Deducations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_BetEmployees_BetEmployeeId",
                table: "Deducations",
                column: "BetEmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_BetEmployees_BetEmployeeId",
                table: "Deducations");

            migrationBuilder.AlterColumn<Guid>(
                name: "BetEmployeeId",
                table: "Deducations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_BetEmployees_BetEmployeeId",
                table: "Deducations",
                column: "BetEmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
