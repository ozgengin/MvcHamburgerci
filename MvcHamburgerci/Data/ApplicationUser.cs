using Microsoft.AspNetCore.Identity;
using MvcHamburgerci.Entities;

namespace MvcHamburgerci.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Siparis> Siparisler { get; set; } = new List<Siparis>();
    }
}
