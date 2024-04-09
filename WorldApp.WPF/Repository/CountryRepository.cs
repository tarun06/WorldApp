using WorldApp.WPF.Database;
using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WorldDBContext _worldDBContext;

        public CountryRepository(WorldDBContext worldDBContext)
        {
            _worldDBContext = worldDBContext;
        }
        public async Task AddCountryAsync(Country country)
        {
            if (string.IsNullOrEmpty(country?.CountryName) && country.Continent is null)
                throw new ArgumentNullException(Resources.Resources.COUNTRY_INVALID);

            if (country?.Continent is null || string.IsNullOrEmpty(country?.Continent?.ContinentName))
                throw new ArgumentNullException(Resources.Resources.COUNTRY_NOTEXIST);

            if (_worldDBContext.Country.Any(x => x.CountryName == country.CountryName))
                throw new ArgumentException(Resources.Resources.COUNTRY_EXIST);

            _worldDBContext.Country.Add(new Country()
            {
                CountryName = country.CountryName,
                Currency = country.Currency,
                Language = country.Language,
                Continent = country.Continent,
                CountryId = country.CountryId,
            });
            await _worldDBContext.SaveChangesAsync();
        }

        public IEnumerable<Country> GetAllCountries() => _worldDBContext.Country.ToList();

        public IEnumerable<Country> GetCountries(int continentId)
        {
            return _worldDBContext.Country.Where(x => x.Continent.ContinentId == continentId).ToList();
        }
    }
}