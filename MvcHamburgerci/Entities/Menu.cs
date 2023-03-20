

namespace MvcHamburgerci.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public string? DosyaAd { get; set; }

        public List<Siparis> Siparisler { get; set; }
    }
}
