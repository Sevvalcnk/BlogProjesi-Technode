using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProjesi.Models
{
    public class Makale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Makale Başlığı")]
        [Required(ErrorMessage = "Başlık kısmını boş bırakamazsınız.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Baslik { get; set; }

        [Display(Name = "Makale İçeriği")]
        [Required(ErrorMessage = "Lütfen içeriği doldurun, boş makale olmaz.")]
        [MinLength(10, ErrorMessage = "İçerik çok kısa, lütfen biraz daha detay yazın.")]
        public string Icerik { get; set; }

        [Display(Name = "Kapak Resmi (URL)")]
        [Required(ErrorMessage = "Makaleniz için bir resim linki yapıştırın.")]
        public string ResimUrl { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        // --- İSTATİSTİK VE ANALİZ VERİLERİ (YENİLER) ---

        [Display(Name = "Okunma Sayısı")]
        public int OkunmaSayisi { get; set; } = 0; // 🔥 Hatayı çözen kısım

        [Display(Name = "Beğeni Sayısı")]
        public int BegeniSayisi { get; set; } = 0;

        [Display(Name = "Tahmini Okuma Süresi (Dk)")]
        public int TahminiSure { get; set; } = 3; // Varsayılan 3 dk olsun

        // --- YÖNETİM AYARLARI ---

        [Display(Name = "Yayında mı?")]
        public bool AktifMi { get; set; } = true; // İşaretli değilse sitede görünmez (Taslak)

        [Display(Name = "Öne Çıkarılsın mı?")]
        public bool OneCikarilsinMi { get; set; } = false; // Vitrin için

        // --- İLİŞKİLER ---

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int KategoriId { get; set; }
        public virtual Kategori? Kategori { get; set; }

        [Display(Name = "Yazar")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Kullanici? Kullanici { get; set; }

        public List<Yorum> Yorumlar { get; set; } = new List<Yorum>();
     

    }
}