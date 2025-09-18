using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PixnoriaStore.Data;
using PixnoriaStore.Models;
using System.Linq;
using System.Collections.Generic;

namespace PixnoriaStore.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly PixnoriaContext _context;

        public FavoritesController(PixnoriaContext context)
        {
            _context = context;
        }

        // Favori ürünler sayfası
        public IActionResult MyFavorites()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var favoriUrunler = _context.Favoriler
                .Include(f => f.Urun)
                .Where(f => f.UserId == userId.Value)
                .Select(f => f.Urun)
                .ToList();

            return View(favoriUrunler);
        }

        // Favori ekleme - AJAX
        [HttpPost]
        [Route("Favorites/Add")]
        public IActionResult Add([FromBody] FavoriteAjaxModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var urunId = model.Id;

            // ---- DÜZELTME ----
            // Ürün veritabanında var mı kontrolü!
            var urun = _context.Urunler.FirstOrDefault(u => u.Id == urunId);

            if (urun == null)
                return Json(new { success = false, message = "Ürün veritabanında yok!" });
            // ---- DÜZELTME ----

            var mevcutFavori = _context.Favoriler
                .FirstOrDefault(f => f.UserId == userId.Value && f.UrunId == urunId);

            if (mevcutFavori == null)
            {
                var favori = new Favori
                {
                    UserId = userId.Value,
                    UrunId = urunId
                };
                _context.Favoriler.Add(favori);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Zaten favorilerde." });
        }

        // Favori silme - AJAX
        [HttpPost]
        [Route("Favorites/Remove")]
        public IActionResult Remove([FromBody] FavoriteAjaxModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var urunId = model.Id;
            var favori = _context.Favoriler
                .FirstOrDefault(f => f.UserId == userId.Value && f.UrunId == urunId);

            if (favori != null)
            {
                _context.Favoriler.Remove(favori);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Favoride bulunamadı." });
        }
    }

    // AJAX ile gönderilen model
    public class FavoriteAjaxModel
    {
        public int Id { get; set; }
    }
}