using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityDeducationCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deducations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotBetEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false),
                    BetEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deducations_BetEmployees_BetEmployeeId",
                        column: x => x.BetEmployeeId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deducations_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deducations_NotBetEmployees_NotBetEmployeeId",
                        column: x => x.NotBetEmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deducations_BetEmployeeId",
                table: "Deducations",
                column: "BetEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Deducations_DocumentId",
                table: "Deducations",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Deducations_NotBetEmployeeId",
                table: "Deducations",
                column: "NotBetEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deducations");
        }
    }
}
