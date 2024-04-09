using WorldApp.WPF.Database;
using WorldApp.WPF.Extensions;
using WorldApp.WPF.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WorldApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private IWindowManager _windowManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            _serviceProvider = new ServiceCollection().
                Register().
                BuildServiceProvider();

            _windowManager = _serviceProvider.GetService<IWindowManager>();
            _windowManager.CreateMainWindow();

            base.OnStartup(e);
        }
    }
}