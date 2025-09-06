using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TedarikciPanel.Data;
using TedarikciPanel.Models;
using System.Linq;

namespace TedarikciPanel.Controllers
{
    public class MusteriController : Controller
    {
        private readonly AppDbContext _context;

        public MusteriController(AppDbContext context)
        {
            _context = context;
        }

        // Ana sayfa
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Musteri")
                return RedirectToAction("Login", "Account");

            return View();
        }

        // Ürünler sayfası
        public IActionResult Urunler()
        {
            if (HttpContext.Session.GetString("Rol") != "Musteri")
                return RedirectToAction("Login", "Account");

            var urunler = _context.Products.ToList(); // Veritabanından ürünleri çekiyoruz
            return View(urunler); // Listeyi view’a gönderiyoruz
        }
    }
}
