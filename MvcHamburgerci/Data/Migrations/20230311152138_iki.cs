using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcHamburgerci.Data.Migrations
{
    public partial class iki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Odendi",
                table: "Siparisler");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Odendi",
                table: "Siparisler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
