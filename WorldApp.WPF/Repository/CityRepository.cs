using WorldApp.WPF.Database;
using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly WorldDBContext _worldDBContext;

        public CityRepository(WorldDBContext worldDBContext)
        {
            _worldDBContext = worldDBContext;
        }

        public async Task AddCityAsync(City city)
        {
            if (string.IsNullOrEmpty(city?.CityName))
                throw new ArgumentNullException(Resources.Resources.CITY_INVALID);

            if (city?.Country is null || string.IsNullOrEmpty(city?.Country?.CountryName))
                throw new ArgumentNullException(Resources.Resources.CITY_NOTEXIST);

            if (_worldDBContext.City.Any(x => x.CityName == city.CityName))
                throw new ArgumentException(Resources.Resources.CITY_EXIST);

            _worldDBContext.City.Add(new City()
            {
                Country = city.Country,
                CityName = city.CityName,
                IsCapital = city.IsCapital,
                Population = city.Population,
            });
            await _worldDBContext.SaveChangesAsync();
        }

        public IEnumerable<City> GetCities(int countryId)
        {
            return _worldDBContext.City.Where(x => x.Country.CountryId == countryId).ToList();
        }
    }
}