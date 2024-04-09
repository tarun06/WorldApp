using WorldApp.WPF.Configuration;
using WorldApp.WPF.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WorldApp.WPF.Database
{
    public class WorldDBContext : DbContext
    {
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Continent> Continent { get; set; }

        public virtual DbSet<Country> Country { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettingsProvider.Config.GetConnectionString("MSSqlConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Continent>().HasData(
                new Continent { ContinentId = 1, ContinentName = "Asia" },
                new Continent { ContinentId = 2, ContinentName = "North America" });
        }
    }
}