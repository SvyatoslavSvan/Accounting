using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class PropDocumentAtPayoutChangedToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "AccrualsNotBetEmployee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "AccrualsBetEmployee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "AccrualsNotBetEmployee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "AccrualsBetEmployee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsBetEmployee_Documents_DocumentId",
                table: "AccrualsBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsNotBetEmployee_Documents_DocumentId",
                table: "AccrualsNotBetEmployee",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
