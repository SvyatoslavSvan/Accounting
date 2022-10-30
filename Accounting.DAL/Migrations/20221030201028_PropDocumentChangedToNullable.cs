using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class PropDocumentChangedToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "Deducations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations",
                column: "DocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "Deducations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deducations_DeducationDocuments_DocumentId",
                table: "Deducations",
                column: "DocumentId",
                principalTable: "DeducationDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
