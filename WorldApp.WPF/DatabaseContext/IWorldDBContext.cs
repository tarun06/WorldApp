using WorldApp.WPF.Model;
using Microsoft.EntityFrameworkCore;

namespace WorldApp.WPF.Database
{
    public interface IWorldDBContext
    {
        DbSet<City> City { get; }
        DbSet<Continent> Continent { get; }

        DbSet<Country> Country { get; }
    }
}