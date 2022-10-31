using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityDeducationBetEmployeeNotBetEmployeeCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deducations");

            migrationBuilder.CreateTable(
                name: "DeducationBetEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeducationBetEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeducationBetEmployees_BetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeducationBetEmployees_DeducationDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DeducationDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeducationNotBetEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeducationNotBetEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeducationNotBetEmployees_DeducationDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DeducationDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeducationNotBetEmployees_NotBetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeducationBetEmployees_DocumentId",
                table: "DeducationBetEmployees",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationBetEmployees_EmployeeId",
                table: "DeducationBetEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationNotBetEmployees_DocumentId",
                table: "DeducationNotBetEmployees",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationNotBetEmployees_EmployeeId",
                table: "DeducationNotBetEmployees",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeducationBetEmployees");

            migrationBuilder.DropTable(
                name: "DeducationNotBetEmployees");

            migrationBuilder.CreateTable(
                name: "Deducations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BetEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NotBetEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deducations_BetEmployees_BetEmployeeId",
                        column: x => x.BetEmployeeId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deducations_DeducationDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DeducationDocuments",
                        principalColumn: "Id");
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
    }
}
