using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TedarikciPanel.Data;
using TedarikciPanel.Helpers;
using TedarikciPanel.Models;

public class SiparisController : Controller
{
    private readonly AppDbContext _context;

    public SiparisController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Urunler()
    {
        var urunler = _context.Products.ToList(); // veritabanından çek
        return View(urunler);
    }

    public IActionResult Gecmis()
    {
        if (HttpContext.Session.GetString("Rol") != "Musteri")
            return RedirectToAction("Login", "Account");

        var musteriId = HttpContext.Session.GetInt32("MusteriId") ?? 0;

        var siparisler = _context.Siparisler
            .Where(s => s.MusteriId == musteriId)
            .Include(s => s.Detaylar)
            .OrderByDescending(s => s.Tarih)
            .ToList();

        return View(siparisler);
    }
    public IActionResult PdfIndir(int id)
    {
        var siparis = _context.Siparisler
            .Include(s => s.Detaylar)  // EKLENDİ
            .FirstOrDefault(s => s.Id == id);

        if (siparis == null)
            return NotFound();

        if (siparis.Detaylar == null || !siparis.Detaylar.Any())
            return BadRequest("Sipariş detayları bulunamadı.");

        var pdfBytes = FaturaPdfGenerator.FaturaOlustur(siparis);
        return File(pdfBytes, "application/pdf", $"Fatura_{siparis.Id}.pdf");
    }

}

