using System.ComponentModel.DataAnnotations;

namespace MvcHamburgerci.Models
{
    public class MalzemeViewModel
    {
        [Required(ErrorMessage = "Malzeme adı boş olamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Malzeme fiyatı boş olamaz.")]
        public decimal? Fiyat { get; set; }
    }
}
