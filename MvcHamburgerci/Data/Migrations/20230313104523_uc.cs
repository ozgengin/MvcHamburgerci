using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcHamburgerci.Data.Migrations
{
    public partial class uc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SiparisTarihi",
                table: "Siparisler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiparisTarihi",
                table: "Siparisler");
        }
    }
}
