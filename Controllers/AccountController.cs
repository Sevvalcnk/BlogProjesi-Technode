using BlogProjesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BlogProjesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // --- GİRİŞ (LOGIN) ---
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Sifre))
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Sifre, false, false);
                    if (result.Succeeded) return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            }
            return View(model);
        }

        // --- KAYIT (REGISTER) ---

        // 🔥 405 HATASINI ÇÖZEN KISIM (GET) 🔥
        [HttpGet]
        public IActionResult Register()
        {
            // Kullanıcı Kayıt Ol butonuna bastığında da Login sayfası açılsın
            // Ama panel "Kayıt" modunda kaysın
            ViewBag.ShowRegisterPanel = true;
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Ad) && !string.IsNullOrEmpty(model.Soyad))
            {
                var kullanici = new Kullanici { UserName = model.Email, Email = model.Email, Ad = model.Ad, Soyad = model.Soyad };
                var sonuc = await _userManager.CreateAsync(kullanici, model.Sifre);

                if (sonuc.Succeeded)
                {
                    await _signInManager.SignInAsync(kullanici, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in sonuc.Errors) ModelState.AddModelError("", error.Description);
            }
            ViewBag.ShowRegisterPanel = true;
            return View("Login", model);
        }

        // --- ÇIKIŞ (LOGOUT) ---
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // --- PROFİL (HESABIM) ---
        [HttpGet]
        public async Task<IActionResult> Profil()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var model = new ProfilViewModel
            {
                Ad = user.Ad,
                Soyad = user.Soyad,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profil(ProfilViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            user.Ad = model.Ad;
            user.Soyad = model.Soyad;

            var sonuc = await _userManager.UpdateAsync(user);

            if (sonuc.Succeeded)
            {
                TempData["Mesaj"] = "✅ Bilgilerin başarıyla güncellendi!";
                return RedirectToAction("Profil");
            }

            foreach (var item in sonuc.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        // --- ŞİFRE İŞLEMLERİ ---
        [HttpGet]
        public IActionResult SifremiUnuttum() => View();

        [HttpPost]
        public async Task<IActionResult> SifremiUnuttum(string email)
        {
            if (string.IsNullOrEmpty(email)) return View();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewBag.Mesaj = "Eğer kayıtlıysa, şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("SifreYenile", "Account", new { token, email = user.Email }, Request.Scheme);
            bool sonuc = MailGonder(user.Email, "Şifre Sıfırlama", $"Merhaba {user.Ad},<br><br>Şifrenizi yenilemek için lütfen <a href='{link}'>BURAYA TIKLAYIN</a>.");
            ViewBag.Mesaj = sonuc ? "Mail başarıyla gönderildi!" : "Mail gönderilirken hata oluştu.";
            return View();
        }

        public IActionResult SifreYenile(string token, string email)
        {
            if (token == null || email == null) return RedirectToAction("Login");
            var model = new SifreYenileViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SifreYenile(SifreYenileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction("Login");
            var sonuc = await _userManager.ResetPasswordAsync(user, model.Token, model.YeniSifre);
            if (sonuc.Succeeded)
            {
                TempData["Basarili"] = "Şifreniz güncellendi! Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            foreach (var error in sonuc.Errors) ModelState.AddModelError("", error.Description);
            return View(model);
        }

        private bool MailGonder(string kime, string baslik, string icerik)
        {
            try
            {
                var gonderenMail = _configuration["EmailAyarlari:GonderenMail"];
                var gonderenSifre = _configuration["EmailAyarlari:GonderenSifre"];
                var smtpSunucu = _configuration["EmailAyarlari:SmtpSunucu"];
                var port = int.Parse(_configuration["EmailAyarlari:Port"]);
                var smtpClient = new SmtpClient(smtpSunucu)
                {
                    Port = port,
                    Credentials = new NetworkCredential(gonderenMail, gonderenSifre),
                    EnableSsl = true,
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(gonderenMail, "TechNode Destek"),
                    Subject = baslik,
                    Body = icerik,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(kime);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MAİL GÖNDERME HATASI: " + ex.Message);
                return false;
            }
        }
    }
}