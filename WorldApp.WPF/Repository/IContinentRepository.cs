using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public interface IContinentRepository
    {
        Task AddContinentAsync(string continentName);

        IEnumerable<Continent> GetContinents();
    }
}