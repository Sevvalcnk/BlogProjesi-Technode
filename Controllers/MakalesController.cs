using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogProjesi.Data;
using BlogProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 

namespace BlogProjesi.Controllers
{
    public class MakalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager; 

        
        public MakalesController(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<IActionResult> Index(string aramaKelimesi, int? kategoriId)
        {
            var makaleler = _context.Makaleler.Include(k => k.Kategori).AsQueryable();

            if (kategoriId.HasValue)
            {
                makaleler = makaleler.Where(s => s.KategoriId == kategoriId);
                var kategoriAdi = _context.Kategoriler.Find(kategoriId.Value)?.Ad;
                ViewData["Baslik"] = kategoriAdi + " Makaleleri";
            }
            else
            {
                ViewData["Baslik"] = "Tüm Makaleler";
            }

            if (!string.IsNullOrEmpty(aramaKelimesi))
            {
                string arama = aramaKelimesi.ToLower();
                makaleler = makaleler.Where(s =>
                    s.Baslik.ToLower().Contains(arama) ||
                    (s.Icerik != null && s.Icerik.ToLower().Contains(arama)) ||
                    (s.Kategori != null && s.Kategori.Ad.ToLower().Contains(arama))
                );
            }

            var sonuc = await makaleler.OrderByDescending(m => m.OlusturulmaTarihi).ToListAsync();
            ViewData["ToplamIcerik"] = sonuc.Count;
            ViewData["AramaKelimesi"] = aramaKelimesi;

            return View(sonuc);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var makale = await _context.Makaleler
                .Include(m => m.Kategori)
                .Include(m => m.Yorumlar)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (makale == null) return NotFound();

            return View(makale);
        }

       
        [Authorize]
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Makale makale)
        {
            if (ModelState.IsValid)
            {
             
                makale.UserId = _userManager.GetUserId(User);
                makale.OlusturulmaTarihi = DateTime.Now;

                _context.Add(makale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", makale.KategoriId);
            return View(makale);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var makale = await _context.Makaleler.FindAsync(id);
            if (makale == null) return NotFound();

           
            var currentUserId = _userManager.GetUserId(User);
            if (makale.UserId != currentUserId)
            {
                TempData["Hata"] = "⛔ HATA: Bu makale sizin e-posta adresinize kayıtlı değil! Başkasının makalesini düzenleyemezsiniz.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", makale.KategoriId);
            return View(makale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Makale makale)
        {
            if (id != makale.Id) return NotFound();

            
            var originalMakale = await _context.Makaleler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (originalMakale == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (originalMakale.UserId != currentUserId)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    makale.UserId = originalMakale.UserId;
                    makale.OlusturulmaTarihi = originalMakale.OlusturulmaTarihi;

                    _context.Update(makale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Makaleler.Any(e => e.Id == makale.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", makale.KategoriId);
            return View(makale);
        }

        
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var makale = await _context.Makaleler
                .Include(m => m.Kategori)
                .Include(m => m.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (makale == null) return NotFound();

            
            var currentUserId = _userManager.GetUserId(User);

            
            if (makale.UserId != currentUserId)
            {
                TempData["Hata"] = "⛔ HATA: Bu makale size ait değil veya sahipsiz eski bir veri. Silemezsiniz!";
                return RedirectToAction(nameof(Index));
            }

            return View(makale);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var makale = await _context.Makaleler.FindAsync(id);

            if (makale != null)
            {
                
                var currentUserId = _userManager.GetUserId(User);

                
                if (makale.UserId != currentUserId)
                {
                    TempData["Hata"] = "⛔ Yetkisiz işlem! Bu makale size ait değil.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Makaleler.Remove(makale);
                await _context.SaveChangesAsync();
                TempData["Basarili"] = "Makale başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Makales/YorumEkle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YorumEkle(int id, string adSoyad, string email, string icerik)
        {
            var yorum = new Yorum
            {
                MakaleId = id,
                AdSoyad = adSoyad,
                Email = email,
                Icerik = icerik,
                Tarih = DateTime.Now
            };

            _context.Yorumlar.Add(yorum);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [Route("Makales/YorumSil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YorumSil(int id, string? dogrulamaEmail)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null) return RedirectToAction("Index");

            var makaleId = yorum.MakaleId;
            bool uyeKendiYorumu = User.Identity.IsAuthenticated && User.Identity.Name == yorum.Email;
            bool misafirDogruMail = !string.IsNullOrEmpty(dogrulamaEmail) &&
                                    yorum.Email.Trim().ToLower() == dogrulamaEmail.Trim().ToLower();

            if (uyeKendiYorumu || misafirDogruMail)
            {
                _context.Yorumlar.Remove(yorum);
                await _context.SaveChangesAsync();
                TempData["Basarili"] = "Yorum başarıyla silindi.";
            }

            return RedirectToAction("Details", new { id = makaleId });
        }

        [HttpPost]
        public async Task<IActionResult> Begen(int id, bool begenildi)
        {
            var makale = await _context.Makaleler.FindAsync(id);
            if (makale == null) return NotFound();

            if (begenildi) makale.BegeniSayisi++;
            else
            {
                makale.BegeniSayisi--;
                if (makale.BegeniSayisi < 0) makale.BegeniSayisi = 0;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, begeniSayisi = makale.BegeniSayisi });
        }
    }
}