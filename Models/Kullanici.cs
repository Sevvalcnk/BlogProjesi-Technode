using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    
    public class Kullanici : IdentityUser
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsim en az 2 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Geçersiz isim! Lütfen sayı veya sembol içermeyen düzgün bir isim girin.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisim en az 2 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Geçersiz soyisim!")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Doğum tarihi boş bırakılamaz.")]
        public DateTime DogumTarihi { get; set; }
    }
}