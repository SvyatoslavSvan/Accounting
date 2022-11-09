using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class DeducationDocumentDeducationDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetEmployeeDeducationDocument");

            migrationBuilder.DropTable(
                name: "DeducationBetEmployees");

            migrationBuilder.DropTable(
                name: "DeducationDocumentNotBetEmployee");

            migrationBuilder.DropTable(
                name: "DeducationNotBetEmployees");

            migrationBuilder.DropTable(
                name: "DeducationDocuments");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Documents");

            migrationBuilder.CreateTable(
                name: "DeducationDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeducationDocuments", x => x.Id);
                });

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
                name: "DeducationBetEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "DeducationNotBetEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAdditional = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_BetEmployeeDeducationDocument_DeducationDocumentsId",
                table: "BetEmployeeDeducationDocument",
                column: "DeducationDocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationBetEmployees_DocumentId",
                table: "DeducationBetEmployees",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationBetEmployees_EmployeeId",
                table: "DeducationBetEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationDocumentNotBetEmployee_NotBetEmployeesId",
                table: "DeducationDocumentNotBetEmployee",
                column: "NotBetEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationNotBetEmployees_DocumentId",
                table: "DeducationNotBetEmployees",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeducationNotBetEmployees_EmployeeId",
                table: "DeducationNotBetEmployees",
                column: "EmployeeId");
        }
    }
}
