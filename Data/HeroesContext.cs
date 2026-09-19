using HeroesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesWeb.Data
{
    public class HeroesContext : DbContext
    {
        public HeroesContext(DbContextOptions<HeroesContext> options) : base(options)
        {
        }

        public DbSet<Heroes> Heroes => Set<Heroes>();
        public DbSet<SuperPoderes> SuperPoderes => Set<SuperPoderes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Un heroe tiene muchos poderes; al borrar el heroe se borran sus poderes (igual que en la base).
            modelBuilder.Entity<SuperPoderes>()
                .HasOne(sp => sp.Heroe)
                .WithMany(h => h.SuperPoderes)
                .HasForeignKey(sp => sp.HeroeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
