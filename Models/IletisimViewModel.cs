using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    public class IletisimViewModel
    {
        [Required(ErrorMessage = "Ad Soyad kısmı boş bırakılamaz.")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "E-Posta adresi zorunludur, lütfen doldurun.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi yazınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu başlığı yazmanız gerekmektedir.")]
        public string Konu { get; set; }

        [Required(ErrorMessage = "Mesaj kutusunu boş bırakamazsınız, lütfen içeriği yazın.")]
        [MinLength(10, ErrorMessage = "Mesajınız çok kısa, lütfen en az 10 karakter yazın.")]
        public string Mesaj { get; set; }

    }
}