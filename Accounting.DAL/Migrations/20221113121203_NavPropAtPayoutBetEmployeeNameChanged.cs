using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class NavPropAtPayoutBetEmployeeNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_BetEmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.DropIndex(
                name: "IX_PayoutsBetEmployee_BetEmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.DropColumn(
                name: "BetEmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsBetEmployee_EmployeeId",
                table: "PayoutsBetEmployee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_EmployeeId",
                table: "PayoutsBetEmployee",
                column: "EmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_EmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.DropIndex(
                name: "IX_PayoutsBetEmployee_EmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.AddColumn<Guid>(
                name: "BetEmployeeId",
                table: "PayoutsBetEmployee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsBetEmployee_BetEmployeeId",
                table: "PayoutsBetEmployee",
                column: "BetEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_BetEmployeeId",
                table: "PayoutsBetEmployee",
                column: "BetEmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
