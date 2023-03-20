using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcHamburgerci.Data;
using MvcHamburgerci.Entities;
using System.Security.Claims;

namespace MvcHamburgerci.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    [Authorize]
    public class MusteriController : Controller
    {
        private readonly ApplicationDbContext _db;
        string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public MusteriController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View(_db.Menuler.ToList());
        }

        public IActionResult OncekiSiparisler()
        {
            if(_db.Siparisler.Include(s => s.SeciliMenusu).Where(x => x.KullaniciId == UserId).Count() <= 0)
            {
                ViewBag.Siparis = "Daha önce sipariş verilmemiştir.";
            }
            return View(_db.Siparisler.Include(s => s.SeciliMenusu).Where(x => x.KullaniciId == UserId).ToList());
        }


        public IActionResult SiparisOlustur()
        {
            var siparis = new Siparis();
            ViewBag.MenuListesi = new SelectList(_db.Menuler, "Id", "Ad");
            ViewBag.MalzemeListesi = _db.EkstraMalzemeler.ToList();

            siparis.Hesapla();

            return View(siparis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SiparisOlustur(Siparis siparis, List<int> ekstraMalzemeler)
        {
            siparis.SeciliMenusu = _db.Menuler.FirstOrDefault(m => m.Id == siparis.SeciliMenusu.Id);
            siparis.EkstraMalzemeleri = _db.EkstraMalzemeler.Where(e => ekstraMalzemeler.Contains(e.Id)).ToList();
            siparis.KullaniciId = UserId;
            siparis.Hesapla();
            _db.Siparisler.Add(siparis);
            _db.SaveChanges();

            return View("SiparisBilgileri", siparis);
            
        }

        public IActionResult SiparisBilgileri(Siparis siparis, List<int> ekstraMalzemeler)
        {
            return View(siparis);
        }

        [HttpPost]
        public IActionResult SiparisBilgileri()
        {
           
            return RedirectToAction("Index");
        }

        public IActionResult SiparisDuzenle(int id)
        {
            var siparis = _db.Siparisler.Include(s => s.SeciliMenusu).Include(s => s.EkstraMalzemeleri).FirstOrDefault(s => s.Id == id);
            ViewBag.MenuListesi = new SelectList(_db.Menuler, "Id", "Ad");
            ViewBag.MalzemeListesi = _db.EkstraMalzemeler.ToList();


            return View(siparis);
        }

        [HttpPost]
        public IActionResult SiparisDuzenle(Siparis siparis, List<int> ekstraMalzemeler)
        {
            var guncellenenSiparis = _db.Siparisler.Include(s => s.SeciliMenusu).Include(s => s.EkstraMalzemeleri).FirstOrDefault(s => s.Id == siparis.Id);
            guncellenenSiparis.SeciliMenusu = _db.Menuler.Find(siparis.SeciliMenusu.Id);
            guncellenenSiparis.EkstraMalzemeleri = _db.EkstraMalzemeler.Where(m => ekstraMalzemeler.Contains(m.Id)).ToList();
            guncellenenSiparis.Boyutu = siparis.Boyutu;
            guncellenenSiparis.Adedi = siparis.Adedi;
            guncellenenSiparis.Hesapla();

            _db.Update(guncellenenSiparis);
            _db.SaveChanges();



            return View("SiparisBilgileri", guncellenenSiparis);
        }

        public IActionResult SiparisSil(int id)
        {
            var siparis = _db.Siparisler.Include(s => s.SeciliMenusu).Include(s => s.EkstraMalzemeleri).FirstOrDefault(s => s.Id == id);
            _db.Siparisler.Remove(siparis);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

       


    }
}
