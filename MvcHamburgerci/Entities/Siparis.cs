using MvcHamburgerci.Data;
using System.Reflection.Metadata.Ecma335;

namespace MvcHamburgerci.Entities
{
    public class Siparis
    {
        public Siparis()
        {
            ToplamTutar = 0;
        }
        public int Id { get; set; }
        public Menu SeciliMenusu { get; set; } = new Menu();

        public List<EkstraMalzeme> EkstraMalzemeleri { get; set; } = new List<EkstraMalzeme>();

        public BoyutEnum Boyutu { get; set; }

        public int Adedi { get; set; }

        public decimal ToplamTutar { get; set; }

        public DateTime SiparisTarihi { get; set; } = DateTime.Now;

        public string KullaniciId { get; set; } = null!;
        public ApplicationUser Kullanici { get; set; } = null!;

        public void Hesapla()
        {
            if (SeciliMenusu != null)
            {
                ToplamTutar = 0;
                ToplamTutar += SeciliMenusu.Fiyat;

                switch (Boyutu)
                {

                    case BoyutEnum.Orta:
                        ToplamTutar += ToplamTutar * 0.1M;
                        break;
                    case BoyutEnum.Buyuk:
                        ToplamTutar += ToplamTutar * 0.2M;
                        break;

                }

                ToplamTutar *= Adedi;

                foreach (EkstraMalzeme item in EkstraMalzemeleri)
                {
                    ToplamTutar += item.Fiyat;
                }
            }
        }
    }
}
