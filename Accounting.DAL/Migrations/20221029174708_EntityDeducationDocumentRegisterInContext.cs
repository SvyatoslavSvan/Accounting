using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class EntityDeducationDocumentRegisterInContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeducationDocument",
                table: "DeducationDocument");

            migrationBuilder.RenameTable(
                name: "DeducationDocument",
                newName: "DeducationDocuments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeducationDocuments",
                table: "DeducationDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "BetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations",
                column: "DocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "NotBetEmployees",
                column: "DeducationDocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "BetEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations");

            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_DeducationDocuments_DeducationDocumentId",
                table: "NotBetEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeducationDocuments",
                table: "DeducationDocuments");

            migrationBuilder.RenameTable(
                name: "DeducationDocuments",
                newName: "DeducationDocument");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeducationDocument",
                table: "DeducationDocument",
                column: "Id");

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
    }
}
