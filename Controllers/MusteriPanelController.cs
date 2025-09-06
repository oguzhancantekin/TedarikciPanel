using Microsoft.AspNetCore.Mvc;

namespace TedarikciPanel.Controllers
{
    public class MusteriPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
// Bu controller, müşteri paneli için kullanılacak.