using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public interface ICountryRepository
    {
        Task AddCountryAsync(Country country);

        IEnumerable<Country> GetAllCountries();

        IEnumerable<Country> GetCountries(int continentId);
    }
}