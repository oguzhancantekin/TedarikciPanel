using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TedarikciPanel.Models;
using TedarikciPanel.Data;
using System.Diagnostics;

namespace TedarikciPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Ana sayfa ? baþvuru formuna yönlendir
        public IActionResult Index()
        {
            return RedirectToAction("BasvuruFormu");
        }

        // GET: Baþvuru Formu
        [HttpGet]
        public IActionResult BasvuruFormu()
        {
            return View();
        }

        // POST: Baþvuru gönderme iþlemi
        [HttpPost]
        public IActionResult BasvuruGonder(CustomerRequest model)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerRequests.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Tesekkurler");
            }

            return View("BasvuruFormu", model);
        }

        // Baþvuru sonrasý teþekkür sayfasý
        public IActionResult Tesekkurler()
        {
            return View();
        }

        // Privacy sayfasý (kalsýn, gerekirse düzenleriz)
        public IActionResult Privacy()
        {
            return View();
        }

        // Hata sayfasý
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
