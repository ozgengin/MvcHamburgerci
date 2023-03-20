

namespace MvcHamburgerci.Entities
{
    public class EkstraMalzeme
    {
        public int Id { get; set; }

        public string Ad { get; set; }

        public decimal Fiyat { get; set; }

        public List<Siparis> Siparisler { get; set; }
    }
}
