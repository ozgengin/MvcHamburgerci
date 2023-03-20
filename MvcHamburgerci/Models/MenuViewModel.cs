using System.ComponentModel.DataAnnotations;

namespace MvcHamburgerci.Models
{
    public class MenuViewModel
    {
        [Required(ErrorMessage = "Menü adı boş olamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Menü fiyatı boş olamaz.")]
        public decimal? Fiyat { get; set; }
        public IFormFile Resim { get; set; }
    }
}
