using Microsoft.EntityFrameworkCore;
using PixnoriaStore.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// SQL Server bağlantısını ekle
builder.Services.AddDbContext<PixnoriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PixnoriaDb")));

// Cookie Authentication ayarları (giriş uzun süre açık kalsın diye)
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // 30 gün oturum açık!
        options.SlidingExpiration = true;
    });

// SESSION yapılandırması (30 gün boyunca oturum açık kalsın)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata yakalama
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();         // SESSION AKTİFLEŞTİRİLDİ
app.UseAuthentication();  // Cookie Auth aktif!
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();