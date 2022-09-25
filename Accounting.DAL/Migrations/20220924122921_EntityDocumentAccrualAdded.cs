using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityDocumentAccrualAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "NotBetEmployees");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "BetEmployees");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "NotBetEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accruals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accruals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accruals_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accruals_NotBetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotBetEmployees_DocumentId",
                table: "NotBetEmployees",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_DocumentId",
                table: "Accruals",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_EmployeeId",
                table: "Accruals",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_Documents_DocumentId",
                table: "NotBetEmployees",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_Documents_DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropTable(
                name: "Accruals");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_NotBetEmployees_DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "NotBetEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "BetEmployees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
