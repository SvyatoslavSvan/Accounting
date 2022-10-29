using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityDeducationDocumentCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_Documents_DocumentId",
                table: "Deducations");

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

            migrationBuilder.CreateTable(
                name: "DeducationDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeducationDocument", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotBetEmployees_DeducationDocumentId",
                table: "NotBetEmployees",
                column: "DeducationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployees_DeducationDocumentId",
                table: "BetEmployees",
                column: "DeducationDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEmployees_DeducationDocument_DeducationDocumentId",
                table: "BetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_DeducationDocument_DocumentId",
                table: "Deducations",
                column: "DocumentId",
                principalTable: "DeducationDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_DeducationDocument_DeducationDocumentId",
                table: "NotBetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocument",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEmployees_DeducationDocument_DeducationDocumentId",
                table: "BetEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_DeducationDocument_DocumentId",
                table: "Deducations");

            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_DeducationDocument_DeducationDocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropTable(
                name: "DeducationDocument");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_Documents_DocumentId",
                table: "Deducations",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
