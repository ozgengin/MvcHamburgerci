using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcHamburgerci.Data;
using MvcHamburgerci.Entities;
using MvcHamburgerci.Models;

namespace MvcHamburgerci.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MalzemeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MalzemeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.EkstraMalzemeler.ToList());
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(MalzemeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var malzeme = new EkstraMalzeme();
                malzeme.Ad = vm.Ad;
                malzeme.Fiyat = (decimal)vm.Fiyat;
                _db.EkstraMalzemeler.Add(malzeme);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Duzenle(int id)
        {
            EkstraMalzeme? malzeme = _db.EkstraMalzemeler.Find(id);
            var vm = new MalzemeViewModel();
            vm.Ad = malzeme.Ad;
            vm.Fiyat = malzeme.Fiyat;
            TempData["Id"] = id;
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Duzenle(MenuViewModel vm)
        {
            EkstraMalzeme? malzeme = _db.EkstraMalzemeler.Find(TempData["Id"]);
            malzeme.Ad = vm.Ad;
            malzeme.Fiyat = (decimal)vm.Fiyat;

            _db.Update(malzeme);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Sil(int id)
        {
            EkstraMalzeme? malzeme = _db.EkstraMalzemeler.Find(id);
            if (malzeme == null) return NotFound();

            return View(malzeme);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            EkstraMalzeme? malzeme = _db.EkstraMalzemeler.Find(id);
            if (malzeme == null)
                return NotFound();

            _db.Remove(malzeme);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
