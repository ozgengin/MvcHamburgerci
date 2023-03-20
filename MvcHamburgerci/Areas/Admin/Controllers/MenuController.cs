using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcHamburgerci.Data;
using MvcHamburgerci.Entities;
using MvcHamburgerci.Models;
using System.Data;

namespace MvcHamburgerci.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MenuController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Menuler.ToList());
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(MenuViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var menu = new Menu();
                if (vm.Resim != null)
                {
                    string dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(vm.Resim.FileName);
                    string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                    using (var stream = new FileStream(kayitYolu, FileMode.Create))
                    {
                        vm.Resim.CopyTo(stream);
                    }

                    menu.DosyaAd = dosyaAd;
                }

                menu.Ad = vm.Ad;
                menu.Fiyat = (decimal)vm.Fiyat;
                _db.Menuler.Add(menu);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Duzenle(int id)
        {
            Menu? menu = _db.Menuler.Find(id);
            var vm = new MenuViewModel();
            vm.Ad = menu.Ad;
            vm.Fiyat = menu.Fiyat;
            TempData["Id"] = id;
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Duzenle(MenuViewModel vm)
        {
            Menu menu = _db.Menuler.Find(TempData["Id"]);
            menu.Ad = vm.Ad;
            menu.Fiyat = (decimal)vm.Fiyat;

            if (vm.Resim != null)
            {
                string dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(vm.Resim.FileName);
                string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                using (var stream = new FileStream(kayitYolu, FileMode.Create))
                {
                    vm.Resim.CopyTo(stream);
                }

                menu.DosyaAd = dosyaAd;
            }

            _db.Update(menu);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Sil(int id)
        {
            Menu menu = _db.Menuler.Find(id);
            if (menu == null) return NotFound();

            return View(menu);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            Menu? menu = _db.Menuler.Find(id);
            if (menu == null)
                return NotFound();
            if (menu.DosyaAd != null)
            {
                string dosyaYolu = Path.Combine(_env.WebRootPath, "img", menu.DosyaAd);
                if (System.IO.File.Exists(dosyaYolu))
                    System.IO.File.Delete(dosyaYolu);
            }
            _db.Remove(menu);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
