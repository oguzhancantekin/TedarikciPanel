using Microsoft.AspNetCore.Mvc;
using TedarikciPanel.Data;
using TedarikciPanel.Models;
using System.Linq;

namespace TedarikciPanel.Controllers
{
    public class CustomerRequestsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Bekleyenler()
        {
            var bekleyenler = _context.CustomerRequests
                .Where(r => !r.IsApproved)
                .ToList();

            return View(bekleyenler);
        }
        // POST: Başvuruyu Onayla
        [HttpPost]
        public IActionResult Onayla(int id)
        {
            var basvuru = _context.CustomerRequests.FirstOrDefault(c => c.Id == id);
            if (basvuru != null)
            {
                // Yeni müşteri nesnesi oluştur
                var musteri = new Musteri
                {
                    AdSoyad = basvuru.AdSoyad,
                    Email = basvuru.Email,
                    Telefon = basvuru.Telefon,
                    SirketAdi = basvuru.SirketAdi,
                    Sifre = basvuru.Sifre,  // şimdilik düz şifre
                    KayitTarihi = DateTime.Now
                };

                _context.Musteriler.Add(musteri);
                _context.CustomerRequests.Remove(basvuru); // başvuruyu sil
                _context.SaveChanges();
            }

            return RedirectToAction("Bekleyenler");
        }

        // POST: Başvuruyu Sil
        [HttpPost]
        public IActionResult Sil(int id)
        {
            var basvuru = _context.CustomerRequests.FirstOrDefault(c => c.Id == id);
            if (basvuru != null)
            {
                _context.CustomerRequests.Remove(basvuru);
                _context.SaveChanges();
            }

            return RedirectToAction("Bekleyenler");
        }



    }
}
