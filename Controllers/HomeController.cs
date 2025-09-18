using Microsoft.AspNetCore.Mvc;

namespace PixnoriaStore.Controllers
{
    public class HomeController : Controller
    {
        // Ana Sayfa
        public IActionResult Index()
        {
            return View();
        }

        // Gizlilik Politikası
        public IActionResult Privacy()
        {
            return View();
        }

        // Sepet Sayfası
        public IActionResult Sepet()
        {
            return View();
        }

        // Hata Sayfası (isteğe bağlı)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}