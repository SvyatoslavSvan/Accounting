using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class DocumentDeletedFromAccrual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Documents_DocumentId",
                table: "Accruals");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "Accruals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "Accruals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Documents_DocumentId",
                table: "Accruals",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
