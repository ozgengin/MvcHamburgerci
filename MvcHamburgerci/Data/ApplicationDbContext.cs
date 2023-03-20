using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcHamburgerci.Entities;
using System.Reflection.Emit;

namespace MvcHamburgerci.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menuler => Set<Menu>();
        public DbSet<EkstraMalzeme> EkstraMalzemeler => Set<EkstraMalzeme>();
        public DbSet<Siparis> Siparisler => Set<Siparis>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var menuler = new List<Menu>()
            {
                new Menu() {Id =1, Ad="Cheeseburger Menü", Fiyat= 85, DosyaAd= "cheeseburger.png"  },
                new Menu() {Id =2, Ad="BigKing Menü", Fiyat= 109, DosyaAd= "big-king.png"  },
                new Menu() {Id =3, Ad="King Chicken Menü", Fiyat= 90, DosyaAd= "king-chicken.png"  },
                new Menu() {Id =4, Ad="Whopper Menü", Fiyat= 115, DosyaAd= "king-chicken.png"  },
                new Menu() {Id =5, Ad="Klasik Gurme Tavuk Menü", Fiyat= 105, DosyaAd= "klasik-gurme-tavuk.png"  },
            };

            modelBuilder.Entity<Menu>().HasData(menuler);

            var ekstraMalzemeler = new List<EkstraMalzeme>()
            {
                new EkstraMalzeme() {Id=1, Ad="Ketçap", Fiyat=2},
                new EkstraMalzeme() {Id=2, Ad="Mayonez", Fiyat=2},
                new EkstraMalzeme() {Id=3, Ad="Ranch Sos", Fiyat=4},
                new EkstraMalzeme() {Id=4, Ad="Buffalo Sos", Fiyat=4},
                new EkstraMalzeme() {Id=5, Ad="Barbekü Sos", Fiyat=4},
            };

            modelBuilder.Entity<EkstraMalzeme>().HasData(ekstraMalzemeler);

            
        }
    }
}