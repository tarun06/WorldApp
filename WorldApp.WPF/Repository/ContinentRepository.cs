using WorldApp.WPF.Database;
using WorldApp.WPF.Model;

namespace WorldApp.WPF.Repository
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly WorldDBContext _worldDBContext;

        public ContinentRepository(WorldDBContext worldDBContext)
        {

            if (worldDBContext is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(worldDBContext)));

            _worldDBContext = worldDBContext;
        }

        public IEnumerable<Continent> GetContinents() => _worldDBContext.Continent.ToList();
        public async Task AddContinentAsync(string continentName)
        {
            if (string.IsNullOrEmpty(continentName))
                throw new ArgumentNullException(Resources.Resources.CONTINENT_INVALID);

            if (_worldDBContext.Continent.Any(x => x.ContinentName == continentName))
                throw new ArgumentException(Resources.Resources.CONTINENT_EXIST);

            _worldDBContext.Continent.Add(new Continent() { ContinentName = continentName });
            await _worldDBContext.SaveChangesAsync();
        }
    }
}