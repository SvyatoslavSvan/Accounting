using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class ManyToManyDocumnetNotBetEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_Documents_DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropIndex(
                name: "IX_NotBetEmployees_DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "NotBetEmployees");

            migrationBuilder.CreateTable(
                name: "DocumentNotBetEmployee",
                columns: table => new
                {
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNotBetEmployee", x => new { x.DocumentsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_DocumentNotBetEmployee_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentNotBetEmployee_NotBetEmployees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentNotBetEmployee_EmployeesId",
                table: "DocumentNotBetEmployee",
                column: "EmployeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentNotBetEmployee");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "NotBetEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotBetEmployees_DocumentId",
                table: "NotBetEmployees",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_Documents_DocumentId",
                table: "NotBetEmployees",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
