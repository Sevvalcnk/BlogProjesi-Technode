using System;

namespace BlogProjesi.Models
{
    public class Yorum
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }

        public string Email { get; set; } 

        public string Icerik { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

        public int MakaleId { get; set; }
        public Makale Makale { get; set; }
    }
}