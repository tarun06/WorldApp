using WorldApp.WPF.Enums;
using WorldApp.WPF.Model;
using WorldApp.WPF.Notifications;
using WorldApp.WPF.Repository;
using WorldApp.WPF.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WorldApp.WPF.ViewModels
{
    internal class WorldDBMainWindowViewModel : ObservableObject
    {
        private readonly WindowManager windowManager;
        private readonly IContinentRepository _continentRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private ObservableCollection<City> _cityInSelectedCountry = new ObservableCollection<City>();
        private ObservableCollection<Continent> _continents;
        private ObservableCollection<Country> _countryInSelectedContinent;

        public WorldDBMainWindowViewModel(WindowManager windowManager, IContinentRepository continentRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            if (continentRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(continentRepository)));
            
            if (countryRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(countryRepository)));

            if (cityRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(cityRepository)));

            if (windowManager is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(windowManager)));

            this.windowManager = windowManager;
            this._continentRepository = continentRepository;
            this._countryRepository = countryRepository;
            this._cityRepository = cityRepository;
            AddCommand = new RelayCommand<AddType>(Add);
            SelectedContinentChangeCommand = new RelayCommand<Continent>(SelectedContinentChange);
            SelectedCountryChangeCommand = new RelayCommand<Country>(SelectedCountryChange);
            WeakReferenceMessenger.Default.Register<RefreshNotification>(this, HandleAddNotification);
            Continents = new ObservableCollection<Continent>(_continentRepository.GetContinents());
        }

        public ICommand AddCommand { get; }

        public ObservableCollection<City> CityInSelectedCountry
        {
            get => _cityInSelectedCountry;
            set => SetProperty(ref _cityInSelectedCountry, value);
        }

        public ObservableCollection<Continent> Continents
        {
            get => _continents;
            set => SetProperty(ref _continents, value);
        }

        public ObservableCollection<Country> CountryInSelectedContinent
        {
            get => _countryInSelectedContinent;
            set => SetProperty(ref _countryInSelectedContinent, value);
        }

        public ICommand SelectedContinentChangeCommand { get; }
        public ICommand SelectedCountryChangeCommand { get; }

        private async void Add(AddType type)
        {
            switch (type)
            {
                case AddType.Continent:
                    await windowManager.ShowDialogAsync<AddContinentsViewModel>();
                    break;

                case AddType.Countries:
                    await windowManager.ShowDialogAsync<AddCountryViewModel>();
                    break;

                case AddType.Cities:
                    await windowManager.ShowDialogAsync<AddCitiesViewModel>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Command does not exist");
            }
        }

        private void HandleAddNotification(object recipient, RefreshNotification addNotification)
        {
            if (addNotification.Value)
                Continents = new ObservableCollection<Continent>(_continentRepository.GetContinents());
        }

        private void SelectedContinentChange(Continent continent)
        {
            CityInSelectedCountry.Clear();

            if (continent is null) return;
            CountryInSelectedContinent = new ObservableCollection<Country>(_countryRepository.GetCountries(continent.ContinentId));
        }

        private void SelectedCountryChange(Country country)
        {
            if (country is null) return;
            CityInSelectedCountry = new ObservableCollection<City>(_cityRepository.GetCities(country.CountryId));
        }
    }
}