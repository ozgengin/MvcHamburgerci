using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcHamburgerci.Data.Migrations
{
    public partial class ilk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EkstraMalzemeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkstraMalzemeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menuler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DosyaAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menuler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeciliMenusuId = table.Column<int>(type: "int", nullable: false),
                    Boyutu = table.Column<int>(type: "int", nullable: false),
                    Adedi = table.Column<int>(type: "int", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Odendi = table.Column<bool>(type: "bit", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siparisler_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Menuler_SeciliMenusuId",
                        column: x => x.SeciliMenusuId,
                        principalTable: "Menuler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EkstraMalzemeSiparis",
                columns: table => new
                {
                    EkstraMalzemeleriId = table.Column<int>(type: "int", nullable: false),
                    SiparislerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkstraMalzemeSiparis", x => new { x.EkstraMalzemeleriId, x.SiparislerId });
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeSiparis_EkstraMalzemeler_EkstraMalzemeleriId",
                        column: x => x.EkstraMalzemeleriId,
                        principalTable: "EkstraMalzemeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeSiparis_Siparisler_SiparislerId",
                        column: x => x.SiparislerId,
                        principalTable: "Siparisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EkstraMalzemeler",
                columns: new[] { "Id", "Ad", "Fiyat" },
                values: new object[,]
                {
                    { 1, "Ketçap", 2m },
                    { 2, "Mayonez", 2m },
                    { 3, "Ranch Sos", 4m },
                    { 4, "Buffalo Sos", 4m },
                    { 5, "Barbekü Sos", 4m }
                });

            migrationBuilder.InsertData(
                table: "Menuler",
                columns: new[] { "Id", "Ad", "DosyaAd", "Fiyat" },
                values: new object[,]
                {
                    { 1, "Cheeseburger Menü", "cheeseburger.png", 85m },
                    { 2, "BigKing Menü", "big-king.png", 109m },
                    { 3, "King Chicken Menü", "king-chicken.png", 90m },
                    { 4, "Whopper Menü", "king-chicken.png", 115m },
                    { 5, "Klasik Gurme Tavuk Menü", "klasik-gurme-tavuk.png", 105m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EkstraMalzemeSiparis_SiparislerId",
                table: "EkstraMalzemeSiparis",
                column: "SiparislerId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciId",
                table: "Siparisler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_SeciliMenusuId",
                table: "Siparisler",
                column: "SeciliMenusuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkstraMalzemeSiparis");

            migrationBuilder.DropTable(
                name: "EkstraMalzemeler");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Menuler");
        }
    }
}
