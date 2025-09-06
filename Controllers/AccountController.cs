using Microsoft.AspNetCore.Mvc;
using TedarikciPanel.Data;
using TedarikciPanel.Models;

namespace TedarikciPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Admin kontrolü
            if (email == "admin@admin.com" && sifre == "12345")
            {
                HttpContext.Session.SetString("Rol", "Admin");
                HttpContext.Session.SetString("AdSoyad", "Admin Kullanıcı");
                return RedirectToAction("Bekleyenler", "CustomerRequests");
            }

            // Müşteri kontrolü
            var musteri = _context.Musteriler.FirstOrDefault(m => m.Email == email && m.Sifre == sifre);
            if (musteri != null)
            {
                HttpContext.Session.SetString("Rol", "Musteri");
                HttpContext.Session.SetString("AdSoyad", musteri.AdSoyad);
                HttpContext.Session.SetInt32("MusteriId", musteri.Id); // ileride kullanırsın
                return RedirectToAction("Index", "Musteri");
            }

            // Giriş başarısız
            ViewBag.Hata = "Email veya şifre hatalı.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // oturumu temizle
            return RedirectToAction("Login", "Account"); // giriş ekranına yönlendir
        }

    }
}
