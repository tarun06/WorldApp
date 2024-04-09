using WorldApp.WPF.Repository;
using WorldApp.WPF.ViewModels;
using WorldApp.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;

namespace WorldApp.WPF.Service
{
    internal class WindowManager(IServiceProvider serviceProvider) : IWindowManager
    {
        private Dispatcher _dispatcher => Application.Current?.Dispatcher;

        public async void CreateMainWindow()
        {
            await _dispatcher.BeginInvoke(DispatcherPriority.Normal, () =>
            {
                WorldDBMainWindow worldDBMainWindow = new WorldDBMainWindow()
                {
                    DataContext = new WorldDBMainWindowViewModel(this,
                            serviceProvider.GetService<IContinentRepository>(),
                            serviceProvider.GetService<ICountryRepository>(),
                            serviceProvider.GetService<ICityRepository>())
                };

                Application.Current.MainWindow = worldDBMainWindow;
                worldDBMainWindow.ShowDialog();
            });
        }

        private TViewModel CreateViewModel<TViewModelType>() where TViewModelType : TViewModel
        {
            if (serviceProvider.GetService(typeof(TViewModelType)) is not TViewModel viewModel)
                throw new ArgumentException(Resources.Resources.VIEWMODEL_INVALID);
            
            return viewModel;
        }


        public async Task ShowDialogAsync<TViewModelType>() where TViewModelType : TViewModel
        {
            var viewModelT = typeof(TViewModelType);

            await _dispatcher.BeginInvoke(DispatcherPriority.Normal, () =>
            {
                var viewmModel = CreateViewModel<TViewModelType>();

                CustomDialog customDialog = new CustomDialog()
                {
                    DataContext = new CustomDialogViewModel(viewmModel)
                };
                customDialog.ShowDialog();
            });
        }

        public async Task ShowNotificationAsync(string message, string caption)
        {
            await _dispatcher.BeginInvoke(DispatcherPriority.Normal, () =>
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK);
            });
        }
    }
}