using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public interface ICityRepository
    {
        Task AddCityAsync(City city);

        IEnumerable<City> GetCities(int countryId);
    }
}