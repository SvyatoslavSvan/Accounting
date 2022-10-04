using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class AccrualsAddedToDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Accruals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_DocumentId",
                table: "Accruals",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Documents_DocumentId",
                table: "Accruals",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Documents_DocumentId",
                table: "Accruals");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_DocumentId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Accruals");
        }
    }
}
