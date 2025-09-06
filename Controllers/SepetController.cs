using Microsoft.AspNetCore.Mvc;
using TedarikciPanel.Models;
using TedarikciPanel.Data;
using Newtonsoft.Json;
using TedarikciPanel.Helpers;

public class SepetController : Controller
{
    private readonly AppDbContext _context;

    public SepetController(AppDbContext context)
    {
        _context = context;
    }

    // Sepeti Görüntüleme
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") != "Musteri")
            return RedirectToAction("Login", "Account");

        var sepet = HttpContext.Session.GetObjectFromJson<List<SepetItem>>("Sepet") ?? new List<SepetItem>();
        return View(sepet);
    }

    // ✅ AJAX ile ürün ekleme
    [HttpPost]
    public IActionResult AddToCart(int urunId)
    {
        var urun = _context.Products.FirstOrDefault(p => p.Id == urunId);
        if (urun == null) return NotFound();

        var sepet = HttpContext.Session.GetObjectFromJson<List<SepetItem>>("Sepet") ?? new List<SepetItem>();

        var mevcut = sepet.FirstOrDefault(x => x.UrunId == urun.Id);
        if (mevcut != null)
            mevcut.Adet++;
        else
            sepet.Add(new SepetItem
            {
                UrunId = urun.Id,
                UrunAd = urun.Ad,
                Fiyat = urun.Fiyat,
                Adet = 1
            });

        HttpContext.Session.SetObjectAsJson("Sepet", sepet);

        // Sayfayı yenilemeden bilgi döndürmek için
        return Json(new { success = true });
    }

    // ✅ Sepetten ürün silme (isteğe bağlı ama yerleştiriyoruz)
   
    [HttpPost]
    public IActionResult Sil(int index)
    {
        var sepet = HttpContext.Session.GetObjectFromJson<List<SepetItem>>("Sepet") ?? new List<SepetItem>();
        if (index >= 0 && index < sepet.Count)
        {
            sepet.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("Sepet", sepet);
        }
        return RedirectToAction("Index");
    }


    // ✅ Siparişi onayla
    [HttpPost]
    public IActionResult Onayla()
    {
        var sepet = HttpContext.Session.GetObjectFromJson<List<SepetItem>>("Sepet");
        if (sepet == null || !sepet.Any()) return RedirectToAction("Index");

        var musteriId = HttpContext.Session.GetInt32("MusteriId") ?? 0;

        var siparis = new Order
        {
            MusteriId = musteriId,
            Tarih = DateTime.Now,
            Durum = "Onaylandı",
            ToplamTutar = sepet.Sum(x => x.Adet * x.Fiyat),
            Detaylar = sepet.Select(x => new OrderDetail
            {
                UrunAd = x.UrunAd,
                Adet = x.Adet,
                Fiyat = x.Fiyat
            }).ToList()
        };

        _context.Siparisler.Add(siparis);
        _context.SaveChanges();

        HttpContext.Session.Remove("Sepet");
        return RedirectToAction("Gecmis", "Siparis");
    }
}
