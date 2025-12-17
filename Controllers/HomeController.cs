using BlogProjesi.Data;
using BlogProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using System.Linq; // LINQ için

namespace BlogProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // --- ANA SAYFA ---
        public async Task<IActionResult> Index()
        {
            // Test amaçlı kategori yoksa ekle (Bu kısmı isterseniz silebilirsiniz, sadece geliştirme içindir)
            if (!await _context.Kategoriler.AnyAsync(x => x.Ad == "Oyun Geliştirme"))
            {
                _context.Kategoriler.Add(new Kategori { Ad = "Oyun Geliştirme" });
                await _context.SaveChangesAsync();
            }

            var kategoriler = await _context.Kategoriler.ToListAsync();
            var sonMakaleler = await _context.Makaleler
                .Include(m => m.Kategori)
                .OrderByDescending(m => m.OlusturulmaTarihi)
                .Take(6)
                .ToListAsync();

            var model = new HomeViewModel
            {
                Kategoriler = kategoriler,
                SonMakaleler = sonMakaleler
            };

            return View(model);
        }

        public IActionResult Privacy() => View();
        public IActionResult Hakkimizda() => View();

        [HttpGet]
        public IActionResult Iletisim() => View();

        [HttpPost]
        public IActionResult Iletisim(IletisimViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Basarili"] = $"Mesajınız alındı {model.AdSoyad}! Teşekkürler.";
                return RedirectToAction("Iletisim");
            }
            return View(model);
        }

        // ==========================================
        // 🔥 GRAFİKSEL ANALİZ PANELİ (BACKEND) 🔥
        // ==========================================
        [Authorize]
        public IActionResult Istatistikler()
        {
            // 1. GENEL KART VERİLERİ

            // Toplam Okunma
            var toplamOkunma = _context.Makaleler.Any() ? _context.Makaleler.Sum(x => x.OkunmaSayisi) : 0;
            ViewBag.ToplamOkunma = toplamOkunma;

            // DÜZELTME: Toplam Beğeni (Görseldeki hatayı gidermek için try-catch kullanıldı)
            try
            {
                var toplamBegeni = _context.Makaleler.Any() ? _context.Makaleler.Sum(x => x.BegeniSayisi) : 0;
                ViewBag.ToplamBegeni = toplamBegeni;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplam beğeni çekilirken hata oluştu.");
                ViewBag.ToplamBegeni = 0;
            }

            // Toplam Üye ve Makale
            ViewBag.ToplamUye = _context.Users.Count();
            ViewBag.ToplamMakale = _context.Makaleler.Count();

            // Rekor Kıran Makale
            var rekorMakale = _context.Makaleler.OrderByDescending(x => x.OkunmaSayisi).FirstOrDefault();
            ViewBag.RekorBaslik = rekorMakale?.Baslik ?? "-";
            ViewBag.RekorSayi = rekorMakale?.OkunmaSayisi ?? 0;

            // 2. PASTA GRAFİK (Kategori Dağılımı)
            var kategoriVerisi = _context.Makaleler
                .Include(x => x.Kategori)
                .GroupBy(x => x.Kategori.Ad)
                .Select(g => new { KategoriAdi = g.Key, Sayi = g.Count() })
                .ToList();

            ViewBag.KatIsimleri = JsonSerializer.Serialize(kategoriVerisi.Select(x => x.KategoriAdi));
            ViewBag.KatSayilari = JsonSerializer.Serialize(kategoriVerisi.Select(x => x.Sayi));

            // 3. ÇUBUK GRAFİK (En Çok Okunan Top 5)
            // 🔥 Yeni Kısaltma Limiti: 35 karakterden sonra kısaltılacak.
            const int BASLIK_LIMITI = 35;

            var topMakaleler = _context.Makaleler
                .OrderByDescending(x => x.OkunmaSayisi)
                .Take(5)
                .Select(x => new { Baslik = x.Baslik, Okunma = x.OkunmaSayisi })
                .ToList();

            // 🔥 DÜZELTME: Başlıkları kısaltarak gönder
            ViewBag.MakBasliklari = JsonSerializer.Serialize(
                topMakaleler.Select(x => Truncate(x.Baslik, BASLIK_LIMITI))
            );
            ViewBag.MakOkunmalari = JsonSerializer.Serialize(topMakaleler.Select(x => x.Okunma));

            // Tablo için liste
            ViewBag.PopulerListe = _context.Makaleler.OrderByDescending(x => x.OkunmaSayisi).Take(5).ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Helper metot olarak HomeController sınıfı içine ekleyebilirsiniz.
        private string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}