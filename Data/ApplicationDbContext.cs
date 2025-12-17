using BlogProjesi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProjesi.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kullanici>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Makale> Makaleler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }

}