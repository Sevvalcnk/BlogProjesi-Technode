using System.Collections.Generic;

namespace BlogProjesi.Models
{
    public class HomeViewModel
    {
        
        public IEnumerable<Kategori> Kategoriler { get; set; }

     
        public IEnumerable<Makale> SonMakaleler { get; set; }
    }
}