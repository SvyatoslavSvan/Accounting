using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class AccrualsTablesChangedToPayouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsBetEmployee_BetEmployees_BetEmployeeId",
                table: "AccrualsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsNotBetEmployee_NotBetEmployees_EmployeeId",
                table: "AccrualsNotBetEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccrualsNotBetEmployee",
                table: "AccrualsNotBetEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccrualsBetEmployee",
                table: "AccrualsBetEmployee");

            migrationBuilder.RenameTable(
                name: "AccrualsNotBetEmployee",
                newName: "PayoutsNotBetEmployee");

            migrationBuilder.RenameTable(
                name: "AccrualsBetEmployee",
                newName: "PayoutsBetEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_AccrualsNotBetEmployee_EmployeeId",
                table: "PayoutsNotBetEmployee",
                newName: "IX_PayoutsNotBetEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_AccrualsNotBetEmployee_DocumentId",
                table: "PayoutsNotBetEmployee",
                newName: "IX_PayoutsNotBetEmployee_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_AccrualsBetEmployee_DocumentId",
                table: "PayoutsBetEmployee",
                newName: "IX_PayoutsBetEmployee_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_AccrualsBetEmployee_BetEmployeeId",
                table: "PayoutsBetEmployee",
                newName: "IX_PayoutsBetEmployee_BetEmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PayoutsNotBetEmployee",
                table: "PayoutsNotBetEmployee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PayoutsBetEmployee",
                table: "PayoutsBetEmployee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_BetEmployeeId",
                table: "PayoutsBetEmployee",
                column: "BetEmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsBetEmployee_Documents_DocumentId",
                table: "PayoutsBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsNotBetEmployee_Documents_DocumentId",
                table: "PayoutsNotBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayoutsNotBetEmployee_NotBetEmployees_EmployeeId",
                table: "PayoutsNotBetEmployee",
                column: "EmployeeId",
                principalTable: "NotBetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsBetEmployee_BetEmployees_BetEmployeeId",
                table: "PayoutsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsBetEmployee_Documents_DocumentId",
                table: "PayoutsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsNotBetEmployee_Documents_DocumentId",
                table: "PayoutsNotBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_PayoutsNotBetEmployee_NotBetEmployees_EmployeeId",
                table: "PayoutsNotBetEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PayoutsNotBetEmployee",
                table: "PayoutsNotBetEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PayoutsBetEmployee",
                table: "PayoutsBetEmployee");

            migrationBuilder.RenameTable(
                name: "PayoutsNotBetEmployee",
                newName: "AccrualsNotBetEmployee");

            migrationBuilder.RenameTable(
                name: "PayoutsBetEmployee",
                newName: "AccrualsBetEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_PayoutsNotBetEmployee_EmployeeId",
                table: "AccrualsNotBetEmployee",
                newName: "IX_AccrualsNotBetEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_PayoutsNotBetEmployee_DocumentId",
                table: "AccrualsNotBetEmployee",
                newName: "IX_AccrualsNotBetEmployee_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_PayoutsBetEmployee_DocumentId",
                table: "AccrualsBetEmployee",
                newName: "IX_AccrualsBetEmployee_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_PayoutsBetEmployee_BetEmployeeId",
                table: "AccrualsBetEmployee",
                newName: "IX_AccrualsBetEmployee_BetEmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccrualsNotBetEmployee",
                table: "AccrualsNotBetEmployee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccrualsBetEmployee",
                table: "AccrualsBetEmployee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsBetEmployee_BetEmployees_BetEmployeeId",
                table: "AccrualsBetEmployee",
                column: "BetEmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsNotBetEmployee_NotBetEmployees_EmployeeId",
                table: "AccrualsNotBetEmployee",
                column: "EmployeeId",
                principalTable: "NotBetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
