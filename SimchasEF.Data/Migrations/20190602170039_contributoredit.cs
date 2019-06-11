using Microsoft.EntityFrameworkCore.Migrations;

namespace SimchasEF.Data.Migrations
{
    public partial class contributoredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Contributors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Contributors",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
