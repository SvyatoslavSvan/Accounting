using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityAccrualBetEmployeeNotBetEmployeeCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accruals");

            migrationBuilder.CreateTable(
                name: "AccrualsBetEmployee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BetEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccrualsBetEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccrualsBetEmployee_BetEmployees_BetEmployeeId",
                        column: x => x.BetEmployeeId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccrualsNotBetEmployee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccrualsNotBetEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccrualsNotBetEmployee_NotBetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsBetEmployee_BetEmployeeId",
                table: "AccrualsBetEmployee",
                column: "BetEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsBetEmployee_DocumentId",
                table: "AccrualsBetEmployee",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsNotBetEmployee_DocumentId",
                table: "AccrualsNotBetEmployee",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsNotBetEmployee_EmployeeId",
                table: "AccrualsNotBetEmployee",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccrualsBetEmployee");

            migrationBuilder.DropTable(
                name: "AccrualsNotBetEmployee");

            migrationBuilder.CreateTable(
                name: "Accruals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accruals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accruals_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accruals_NotBetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_DocumentId",
                table: "Accruals",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_EmployeeId",
                table: "Accruals",
                column: "EmployeeId");
        }
    }
}
