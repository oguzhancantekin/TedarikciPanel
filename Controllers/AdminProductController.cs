using Microsoft.AspNetCore.Mvc;
using TedarikciPanel.Data;
using TedarikciPanel.Models;
using System.Linq;

namespace TedarikciPanel.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly AppDbContext _context;

        public AdminProductController(AppDbContext context)
        {
            _context = context;
        }
        // GET: /AdminProduct
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult UrunEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UrunEkle(Product model, IFormFile Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.Length > 0)
                {
                    var dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(Resim.FileName);
                    var kayitYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAd);

                    using (var stream = new FileStream(kayitYolu, FileMode.Create))
                    {
                        Resim.CopyTo(stream);
                    }

                    model.ResimYolu = "/images/" + dosyaAd;
                }

                _context.Products.Add(model);
                _context.SaveChanges();
                ViewBag.Mesaj = "Ürün başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var urun = _context.Products.FirstOrDefault(p => p.Id == id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        [HttpPost]
        public IActionResult Edit(Product model, IFormFile? Resim)
        {
            var urun = _context.Products.FirstOrDefault(p => p.Id == model.Id);
            if (urun == null)
            {
                return NotFound();
            }

            // Güncelleme
            urun.Ad = model.Ad;
            urun.Aciklama = model.Aciklama;
            urun.Fiyat = model.Fiyat;
            urun.Stok = model.Stok;

            // Yeni resim yüklendiyse eskiyi silip yenisini ekle
            if (Resim != null)
            {
                // Eski resmi sil (varsa)
                if (!string.IsNullOrEmpty(urun.ResimYolu))
                {
                    var eskiYol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", urun.ResimYolu.TrimStart('/'));
                    if (System.IO.File.Exists(eskiYol))
                        System.IO.File.Delete(eskiYol);
                }

                var uzanti = Path.GetExtension(Resim.FileName);
                var yeniDosyaAd = Guid.NewGuid().ToString() + uzanti;
                var kayitYolu = Path.Combine("wwwroot/images", yeniDosyaAd);
                using (var stream = new FileStream(kayitYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }

                urun.ResimYolu = "/images/" + yeniDosyaAd;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Sil(int id)
        {
            var urun = _context.Products.FirstOrDefault(p => p.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            // Eğer resim varsa fiziksel dosyasını da sil
            if (!string.IsNullOrEmpty(urun.ResimYolu))
            {
                var fizikselYol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", urun.ResimYolu.TrimStart('/'));
                if (System.IO.File.Exists(fizikselYol))
                {
                    System.IO.File.Delete(fizikselYol);
                }
            }

            _context.Products.Remove(urun);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }






    }
}
