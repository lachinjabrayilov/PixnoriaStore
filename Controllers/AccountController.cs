using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using PixnoriaStore.Models;
using PixnoriaStore.Data;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PixnoriaStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly PixnoriaContext _context;
        private const string SessionKeyUserId = "UserId";

        public AccountController(PixnoriaContext context)
        {
            _context = context;
        }

        // Üye Ol - GET
        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        // Üye Ol - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (string.IsNullOrWhiteSpace(model.Ad) ||
                string.IsNullOrWhiteSpace(model.Soyad) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Sifre))
            {
                ViewBag.Error = "Lütfen tüm alanları doğru doldurun.";
                return View(model);
            }

            if (_context.Users.Any(u => u.Email != null && u.Email.ToLower() == model.Email.ToLower()))
            {
                ViewBag.Error = "Bu email ile zaten kayıtlı bir kullanıcı var.";
                return View(model);
            }

            model.Email = model.Email?.Trim();
            model.Sifre = model.Sifre?.Trim();
            model.Ad = model.Ad?.Trim();
            model.Soyad = model.Soyad?.Trim();

            model.KayitTarihi = DateTime.Now;
            model.SonGirisTarihi = DateTime.Now;

            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();

                // Session ve cookie authentication
                HttpContext.Session.SetInt32(SessionKeyUserId, model.Id);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim("UserId", model.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                HttpContext.SignInAsync("MyCookieAuth",
                    new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Kayıt sırasında hata oluştu: " + ex.Message;
                return View(model);
            }
        }

        // Giriş Yap - GET
        [HttpGet]
        public IActionResult Login()
        {
            // Her GET isteğinde temiz bir model gönderiyoruz
            return View(new User());
        }

        // Giriş Yap - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Sifre)
        {
            var errorModel = new User { Email = Email ?? "" };

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Sifre))
            {
                ViewBag.Error = "Lütfen e-posta ve şifre alanlarını doldurun.";
                return View(errorModel);
            }

            try
            {
                string trimmedEmail = Email?.Trim().ToLower();
                string trimmedSifre = Sifre?.Trim();

                var user = _context.Users
                    .FirstOrDefault(u =>
                        u.Email.ToLower() == trimmedEmail &&
                        u.Sifre == trimmedSifre);

                if (user == null)
                {
                    ViewBag.Error = "E-posta veya şifre yanlış.";
                    return View(errorModel);
                }

                // Cookie Authentication Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserAd", user.Ad ?? "");
                HttpContext.Session.SetInt32(SessionKeyUserId, user.Id);

                user.SonGirisTarihi = DateTime.Now;
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.Error = "Sistem hatası oluştu. Lütfen tekrar deneyin.";
                return View(errorModel);
            }
        }

        // Çıkış Yap - GET
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Tüm session ve cookie authentication temizle
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync("MyCookieAuth");

            // ANA SAYFAYA YÖNLENDİRİYORUZ!
            return RedirectToAction("Index", "Home");
        }
    }
}