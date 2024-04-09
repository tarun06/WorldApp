using WorldApp.WPF.Model;
using WorldApp.WPF.Repository;
using WorldApp.WPF.Service;

namespace WorldApp.WPF.ViewModels
{
    public class AddCountryViewModel : TViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly ICountryRepository _countryRepository;
        private string _country;
        private string _currency;
        private string _language;
        private Continent _selectedContinent;

        public AddCountryViewModel(ICountryRepository countryRepository, IContinentRepository continentRepository, IWindowManager windowManager)
        {
            if (countryRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(countryRepository)));

            if (continentRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(continentRepository)));

            if (windowManager is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(windowManager)));

            this._countryRepository = countryRepository;
            this._windowManager = windowManager;
            Continents = continentRepository.GetContinents();
        }

        public IEnumerable<Continent> Continents { get; set; }

        public string CountryName
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public string Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }

        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }

        public override string Name => Resources.Resources.ADD_COUNTRY;

        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set => SetProperty(ref _selectedContinent, value);
        }

        public override async Task AddAsync()
        {
            try
            {
                if (IsInValid())
                    return;

                await _countryRepository.AddCountryAsync(new Country()
                {
                    CountryName = CountryName,
                    Currency = Currency,
                    Language = Language,
                    Continent = SelectedContinent
                });

                await _windowManager.ShowNotificationAsync(Resources.Resources.COUNTRY_ADDED, Resources.Resources.SUCCESS);
            }
            catch (Exception ex)
            {
                await _windowManager.ShowNotificationAsync(ex.Message, Resources.Resources.ERROR);
            }
        }

        private bool IsInValid()
        {
            var flag = string.IsNullOrEmpty(CountryName) ||
                        string.IsNullOrEmpty(Currency) ||
                        string.IsNullOrEmpty(Language) ||
                        string.IsNullOrEmpty(SelectedContinent?.ContinentName);
            return flag;
        }
    }
}