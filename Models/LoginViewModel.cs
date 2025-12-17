using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    public class LoginViewModel
    {
        
        [Required(ErrorMessage = "E-Posta zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        public bool BeniHatirla { get; set; }

        
        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        [DataType(DataType.Password)]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string? SifreTekrar { get; set; }
    }
}