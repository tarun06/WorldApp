using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorldApp.WPF.Configuration
{
    public static class AppSettingsProvider
    {
        public static IConfiguration Config {  get; } 

        static AppSettingsProvider()
        {
            Config = new ConfigurationBuilder()
                      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                      .AddJsonFile("appsettings.json")
                      .Build();
        }
    }
}