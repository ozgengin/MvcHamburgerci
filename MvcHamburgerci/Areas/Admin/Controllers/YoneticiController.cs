using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcHamburgerci.Data;
using System.Data;

namespace MvcHamburgerci.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class YoneticiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public YoneticiController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KullaniciSiparisleri()
        {
            return View(_db.Siparisler.Include(s => s.SeciliMenusu).Include(s => s.Kullanici).ToList());
        }
    }
}
