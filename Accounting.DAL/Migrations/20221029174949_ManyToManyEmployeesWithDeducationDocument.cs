using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class ManyToManyEmployeesWithDeducationDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "BetEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropIndex(
                name: "IX_NotBetEmployees_DeducationDocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BetEmployees_DeducationDocumentId",
                table: "BetEmployees");

            migrationBuilder.DropColumn(
                name: "DeducationDocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropColumn(
                name: "DeducationDocumentId",
                table: "BetEmployees");

            migrationBuilder.CreateTable(
                name: "BetEmployeeDeducationDocument",
                columns: table => new
                {
                    BetEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeducationDocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEmployeeDeducationDocument", x => new { x.BetEmployeesId, x.DeducationDocumentsId });
                    table.ForeignKey(
                        name: "FK_BetEmployeeDeducationDocument_BetEmployees_BetEmployeesId",
                        column: x => x.BetEmployeesId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetEmployeeDeducationDocument_DeducationDocuments_DeducationDocumentsId",
                        column: x => x.DeducationDocumentsId,
                        principalTable: "DeducationDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeducationDocumentNotBetEmployee",
                columns: table => new
                {
                    DeducationDocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotBetEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeducationDocumentNotBetEmployee", x => new { x.DeducationDocumentsId, x.NotBetEmployeesId });
                    table.ForeignKey(
                        name: "FK_DeducationDocumentNotBetEmployee_DeducationDocuments_DeducationDocumentsId",
                        column: x => x.DeducationDocumentsId,
                        principalTable: "DeducationDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeducationDocumentNotBetEmployee_NotBetEmployees_NotBetEmployeesId",
                        column: x => x.NotBetEmployeesId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployeeDeducationDocument_DeducationDocumentsId",
                table: "BetEmployeeDeducationDocument",
                column: "DeducationDocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationDocumentNotBetEmployee_NotBetEmployeesId",
                table: "DeducationDocumentNotBetEmployee",
                column: "NotBetEmployeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetEmployeeDeducationDocument");

            migrationBuilder.DropTable(
                name: "DeducationDocumentNotBetEmployee");

            migrationBuilder.AddColumn<Guid>(
                name: "DeducationDocumentId",
                table: "NotBetEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeducationDocumentId",
                table: "BetEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotBetEmployees_DeducationDocumentId",
                table: "NotBetEmployees",
                column: "DeducationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployees_DeducationDocumentId",
                table: "BetEmployees",
                column: "DeducationDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "BetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "NotBetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id");
        }
    }
}
