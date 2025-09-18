using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixnoriaStore.Models;
using PixnoriaStore.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
namespace PixnoriaStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly PixnoriaContext _context;

        public OrderController(PixnoriaContext context)
        {
            _context = context;
        }

        // Tüm siparişlerim sayfası
        public IActionResult AllOrders(int pageNumber = 1, int pageSize = 10)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Kullanıcıya ait siparişler
            var orders = _context.Siparisler
                .Where(s => s.UserId == userId.Value)
                .Include(s => s.SiparisDetaylar)
                    .ThenInclude(sd => sd.Urun)
                .OrderByDescending(s => s.Tarih)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // ViewModel
            var model = orders.Select(s => new SiparisViewModel
            {
                Id = s.Id,
                Tarih = s.Tarih,
                Alici = s.Alici ?? "Mevcut Kullanıcı",
                Ozet = $"{s.SiparisDetaylar?.Count ?? 0} Ürün, {s.SiparisDetaylar?.Sum(u => u.Adet) ?? 0} Adet",
                Toplam = s.SiparisDetaylar != null && s.SiparisDetaylar.Count > 0
                    ? s.SiparisDetaylar.Sum(u => u.Fiyat * u.Adet)
                    : 0,
                TeslimDurumAciklama = s.TeslimDurumu ?? "Tüm ürünler teslim edildi",
                Urunler = s.SiparisDetaylar != null && s.SiparisDetaylar.Count > 0
                    ? s.SiparisDetaylar
                        .Where(u => u.Urun != null)
                        .Select(u => new UrunViewModel
                        {
                            Ad = u.Urun.Ad,
                            ResimUrl = u.Urun.ResimUrl,
                            Adet = u.Adet,
                            Fiyat = u.Fiyat,
                            // Buraya Id eklemek istiyorsan: Id = u.Urun.Id
                        }).ToList()
                    : new List<UrunViewModel>()
            }).ToList();

            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            return View(model);
        }

        // Siparişi tamamlama (Sepet ürünlerinden sipariş oluşturur)
        [HttpPost]
        public IActionResult CompleteOrder([FromBody] List<SepetUrunu> sepetUrunleri)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            if (sepetUrunleri == null || sepetUrunleri.Count == 0)
                return Json(new { success = false, message = "Sepetiniz boş." });

            var yeniSiparis = new Siparis
            {
                UserId = userId.Value,
                Tarih = DateTime.Now,
                Alici = "Mevcut Kullanıcı",
                TeslimDurumu = "Hazırlanıyor",
                SiparisDetaylar = new List<SiparisDetay>()
            };

            var urunIds = sepetUrunleri.Select(x => x.UrunId).ToList();
            var urunler = _context.Urunler
                .Where(u => urunIds.Contains(u.Id))
                .ToList();

            foreach (var item in sepetUrunleri)
            {
                var urun = urunler.FirstOrDefault(u => u.Id == item.UrunId);
                if (urun == null) continue;

                yeniSiparis.SiparisDetaylar.Add(new SiparisDetay
                {
                    UrunId = urun.Id,
                    Adet = item.Adet,
                    Fiyat = urun.Fiyat
                });
            }

            _context.Siparisler.Add(yeniSiparis);
            _context.SaveChanges();

            return Json(new { success = true, message = "Sipariş başarıyla oluşturuldu!" });
        }

        // Siparişteki ürünü silmek için (AJAX endpoint)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOrderProduct([FromBody] DeleteOrderProductModel model)
        {
            if (model == null)
                return Json(new { success = false, message = "Geçersiz istek." });

            var siparis = _context.Siparisler
                .Include(s => s.SiparisDetaylar)
                .FirstOrDefault(s => s.Id == model.OrderId);

            if (siparis == null)
                return Json(new { success = false, message = "Sipariş bulunamadı." });

            var detay = siparis.SiparisDetaylar.FirstOrDefault(d => d.UrunId == model.ProductId);
            if (detay == null)
                return Json(new { success = false, message = "Ürün bu siparişte yok." });

            siparis.SiparisDetaylar.Remove(detay);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // Model: Sepetten gelen ürünler için (Sepet → Sipariş)
        public class SepetUrunu
        {
            public int UrunId { get; set; }
            public int Adet { get; set; }
        }

        // Model: Siparişten ürün silmek için (AJAX)
        public class DeleteOrderProductModel
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
        }
    }
}