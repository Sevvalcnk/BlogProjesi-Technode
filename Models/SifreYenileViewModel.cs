using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    public class SifreYenileViewModel
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string YeniSifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("YeniSifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string SifreTekrar { get; set; }
    }
}