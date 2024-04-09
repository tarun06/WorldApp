using WorldApp.WPF.Model;
using WorldApp.WPF.Repository;
using WorldApp.WPF.Service;

namespace WorldApp.WPF.ViewModels
{
    public class AddCitiesViewModel : TViewModel
    {
        private readonly ICityRepository _cityRepository;
        private readonly IWindowManager _windowManager;
        private string _cityName;

        private bool _isCapital;
        private long _population;

        private Country _selectedCountry;

        public AddCitiesViewModel(ICityRepository cityRepository, ICountryRepository countryRepository, IWindowManager windowManager)
        {
            if (cityRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(cityRepository)));

            if (countryRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(countryRepository)));

            if (windowManager is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(windowManager)));

            this._cityRepository = cityRepository;
            this._windowManager = windowManager;
            Countries = countryRepository.GetAllCountries();
        }

        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        public IEnumerable<Country> Countries { get; set; }

        public bool IsCapital
        {
            get => _isCapital;
            set => SetProperty(ref _isCapital, value);
        }

        public override string Name => Resources.Resources.ADD_CITY;

        public long Population
        {
            get => _population;
            set => SetProperty(ref _population, value);
        }

        public Country SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        public override async Task AddAsync()
        {
            try
            {
                if (IsInValid())
                    return;

                await _cityRepository.AddCityAsync(new City()
                {
                    CityName = CityName,
                    Country = SelectedCountry,
                    IsCapital = IsCapital,
                    Population = Population
                });
                await _windowManager.ShowNotificationAsync(Resources.Resources.CITY_ADDED, Resources.Resources.SUCCESS);
            }
            catch (Exception ex)
            {
                await _windowManager.ShowNotificationAsync(ex.Message, Resources.Resources.ERROR);
            }
        }

        private bool IsInValid()
        {
            var flag = string.IsNullOrEmpty(CityName) ||
                         Population <= 0 ||
                         string.IsNullOrEmpty(SelectedCountry?.CountryName);
            return flag;
        }
    }
}