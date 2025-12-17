using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    public class KayitViewModel
    {
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Geçersiz isim! Lütfen sayı veya sembol içermeyen düzgün bir isim girin.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen soyisminizi giriniz.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Geçersiz soyisim!")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-Posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateTime DogumTarihi { get; set; }
    }
}