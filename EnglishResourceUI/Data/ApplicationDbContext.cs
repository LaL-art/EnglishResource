using EnglishResourceUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishResourceUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudyFile> StudyFiles { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Favourites> Favoritess { get; set; }
        public DbSet<FavouritesDetail> FavoritesDetails { get; set;}
        public DbSet<Topic> Topics { get; set; } 
    }
}
