using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.DAL.Migrations
{
    public partial class PropHoursTypeChangedFromIntToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Hours",
                table: "WorkDays",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "WorkDays",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
