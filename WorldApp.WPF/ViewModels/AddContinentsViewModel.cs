using WorldApp.WPF.Repository;
using WorldApp.WPF.Service;

namespace WorldApp.WPF.ViewModels
{


    internal class AddContinentsViewModel : TViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IContinentRepository _continentRepository;
        private string _continent;

        public AddContinentsViewModel(IContinentRepository continentRepository, IWindowManager windowManager)
        {
            if (continentRepository is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(continentRepository)));

            if (windowManager is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(windowManager)));

            _continentRepository = continentRepository;
            this._windowManager = windowManager;
        }

        public string Continent
        {
            get => _continent;
            set => SetProperty(ref _continent, value);
        }

        public override string Name => Resources.Resources.ADD_CONTINENT;

        public override async Task AddAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Continent))
                    return;

                await _continentRepository.AddContinentAsync(Continent);
                await _windowManager.ShowNotificationAsync(Resources.Resources.CONTINENT_ADDED, Resources.Resources.SUCCESS);
            }
            catch (Exception ex)
            {
                await _windowManager.ShowNotificationAsync(ex.Message, Resources.Resources.ERROR);
            }
        }
    }
}