using System.ComponentModel.DataAnnotations;

namespace BlogProjesi.Models
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kategori Adı")]
        public string Ad { get; set; } 

        
        public virtual ICollection<Makale> Makaleler { get; set; }
    }
}