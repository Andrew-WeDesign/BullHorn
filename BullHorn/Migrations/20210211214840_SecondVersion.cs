using Microsoft.EntityFrameworkCore.Migrations;

namespace BullHorn.Migrations
{
    public partial class SecondVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EMA20Daily",
                table: "DailyOHLCs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SMA180Daily",
                table: "DailyOHLCs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SMA9Daily",
                table: "DailyOHLCs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMA20Daily",
                table: "DailyOHLCs");

            migrationBuilder.DropColumn(
                name: "SMA180Daily",
                table: "DailyOHLCs");

            migrationBuilder.DropColumn(
                name: "SMA9Daily",
                table: "DailyOHLCs");
        }
    }
}
