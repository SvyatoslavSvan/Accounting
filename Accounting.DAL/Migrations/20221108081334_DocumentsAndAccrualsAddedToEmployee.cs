using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class DocumentsAndAccrualsAddedToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentNotBetEmployee_NotBetEmployees_EmployeesId",
                table: "DocumentNotBetEmployee");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "DocumentNotBetEmployee",
                newName: "NotBetEmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentNotBetEmployee_EmployeesId",
                table: "DocumentNotBetEmployee",
                newName: "IX_DocumentNotBetEmployee_NotBetEmployeesId");

            migrationBuilder.CreateTable(
                name: "BetEmployeeDocument",
                columns: table => new
                {
                    BetEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEmployeeDocument", x => new { x.BetEmployeesId, x.DocumentsId });
                    table.ForeignKey(
                        name: "FK_BetEmployeeDocument_BetEmployees_BetEmployeesId",
                        column: x => x.BetEmployeesId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetEmployeeDocument_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployeeDocument_DocumentsId",
                table: "BetEmployeeDocument",
                column: "DocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentNotBetEmployee_NotBetEmployees_NotBetEmployeesId",
                table: "DocumentNotBetEmployee",
                column: "NotBetEmployeesId",
                principalTable: "NotBetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentNotBetEmployee_NotBetEmployees_NotBetEmployeesId",
                table: "DocumentNotBetEmployee");

            migrationBuilder.DropTable(
                name: "BetEmployeeDocument");

            migrationBuilder.RenameColumn(
                name: "NotBetEmployeesId",
                table: "DocumentNotBetEmployee",
                newName: "EmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentNotBetEmployee_NotBetEmployeesId",
                table: "DocumentNotBetEmployee",
                newName: "IX_DocumentNotBetEmployee_EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentNotBetEmployee_NotBetEmployees_EmployeesId",
                table: "DocumentNotBetEmployee",
                column: "EmployeesId",
                principalTable: "NotBetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
