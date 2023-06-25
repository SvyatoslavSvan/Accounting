using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class TphEmployeeAddedAndPayoutCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEmployeeTimesheet_BetEmployees_EmployeesId",
                table: "BetEmployeeTimesheet");

            migrationBuilder.DropForeignKey(
                name: "FK_NotBetEmployees_Groups_GroupId",
                table: "NotBetEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_BetEmployees_EmployeeId",
                table: "WorkDays");

            migrationBuilder.DropTable(
                name: "BetEmployeeDocument");

            migrationBuilder.DropTable(
                name: "DocumentNotBetEmployee");

            migrationBuilder.DropTable(
                name: "PayoutsBetEmployee");

            migrationBuilder.DropTable(
                name: "PayoutsNotBetEmployee");

            migrationBuilder.DropTable(
                name: "BetEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotBetEmployees",
                table: "NotBetEmployees");

            migrationBuilder.RenameTable(
                name: "NotBetEmployees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_NotBetEmployees_GroupId",
                table: "Employees",
                newName: "IX_Employees_GroupId");

            migrationBuilder.AddColumn<decimal>(
                name: "Bet",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DocumentEmployeeBase",
                columns: table => new
                {
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEmployeeBase", x => new { x.DocumentsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_DocumentEmployeeBase_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEmployeeBase_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payouts",
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
                    table.PrimaryKey("PK_Payouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payouts_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payouts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEmployeeBase_EmployeesId",
                table: "DocumentEmployeeBase",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Payouts_DocumentId",
                table: "Payouts",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payouts_EmployeeId",
                table: "Payouts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEmployeeTimesheet_Employees_EmployeesId",
                table: "BetEmployeeTimesheet",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Groups_GroupId",
                table: "Employees",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Employees_EmployeeId",
                table: "WorkDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEmployeeTimesheet_Employees_EmployeesId",
                table: "BetEmployeeTimesheet");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Groups_GroupId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Employees_EmployeeId",
                table: "WorkDays");

            migrationBuilder.DropTable(
                name: "DocumentEmployeeBase");

            migrationBuilder.DropTable(
                name: "Payouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Bet",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "NotBetEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_GroupId",
                table: "NotBetEmployees",
                newName: "IX_NotBetEmployees_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotBetEmployees",
                table: "NotBetEmployees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BetEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Premium = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetEmployees_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentNotBetEmployee",
                columns: table => new
                {
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotBetEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNotBetEmployee", x => new { x.DocumentsId, x.NotBetEmployeesId });
                    table.ForeignKey(
                        name: "FK_DocumentNotBetEmployee_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentNotBetEmployee_NotBetEmployees_NotBetEmployeesId",
                        column: x => x.NotBetEmployeesId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayoutsNotBetEmployee",
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
                    table.PrimaryKey("PK_PayoutsNotBetEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayoutsNotBetEmployee_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayoutsNotBetEmployee_NotBetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "NotBetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BetEmployeeDocument",
                columns: table => new
                {
                    BetEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEmployeeDocument", x => new { x.BetEmployeesId, x.DocumentsId });
                    table.ForeignKey(
                        name: "FK_BetEmployeeDocument_BetEmployees_BetEmployeesId",
                        column: x => x.BetEmployeesId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetEmployeeDocument_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayoutsBetEmployee",
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
                    table.PrimaryKey("PK_PayoutsBetEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayoutsBetEmployee_BetEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "BetEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayoutsBetEmployee_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployeeDocument_DocumentsId",
                table: "BetEmployeeDocument",
                column: "DocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_BetEmployees_GroupId",
                table: "BetEmployees",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentNotBetEmployee_NotBetEmployeesId",
                table: "DocumentNotBetEmployee",
                column: "NotBetEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsBetEmployee_DocumentId",
                table: "PayoutsBetEmployee",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsBetEmployee_EmployeeId",
                table: "PayoutsBetEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsNotBetEmployee_DocumentId",
                table: "PayoutsNotBetEmployee",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutsNotBetEmployee_EmployeeId",
                table: "PayoutsNotBetEmployee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEmployeeTimesheet_BetEmployees_EmployeesId",
                table: "BetEmployeeTimesheet",
                column: "EmployeesId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotBetEmployees_Groups_GroupId",
                table: "NotBetEmployees",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_BetEmployees_EmployeeId",
                table: "WorkDays",
                column: "EmployeeId",
                principalTable: "BetEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
