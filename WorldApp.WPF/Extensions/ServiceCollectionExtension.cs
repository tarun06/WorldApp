using WorldApp.WPF.Database;
using WorldApp.WPF.Repository;
using WorldApp.WPF.Service;
using WorldApp.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace WorldApp.WPF.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection Register(this IServiceCollection sc)
        {
            RegisterServices(sc);
            RegisterViewModel(sc);
            return sc;
        }

        private static void RegisterViewModel(IServiceCollection sc)
        {
            sc.AddTransient<AddContinentsViewModel>();
            sc.AddTransient<AddCountryViewModel>();
            sc.AddTransient<AddCitiesViewModel>();
        }

        private static void RegisterServices(IServiceCollection sc)
        {
            sc.AddSingleton<WorldDBContext>();
            sc.AddSingleton<ICountryRepository, CountryRepository>();
            sc.AddSingleton<IContinentRepository, ContinentRepository>();
            sc.AddSingleton<ICityRepository, CityRepository>();
            sc.AddSingleton<IWindowManager, WindowManager>();
        }
    }
}