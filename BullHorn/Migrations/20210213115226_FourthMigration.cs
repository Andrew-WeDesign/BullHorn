using Microsoft.EntityFrameworkCore.Migrations;

namespace BullHorn.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RSI12Daily",
                table: "DailyOHLCs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RSI12Daily",
                table: "DailyOHLCs");
        }
    }
}
