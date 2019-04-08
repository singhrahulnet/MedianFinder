using MedianFinder.Managers;
using MedianFinder.Models;
using MedianFinder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace MedianFinder
{
    public class Startup
    {
        public static SourceFolderSettings Settings { get; private set; }
        
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ConfigureServices()
        {
            //Build configuration
            IConfiguration configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build();

            //Add services to IoC container
            _serviceProvider = new ServiceCollection()
                                               .AddSingleton(configuration)
                                               .AddSingleton<IConfigService, ConfigService>()
                                               .AddScoped<ICalculationService, CalculationService>()                                               
                                               .AddScoped<IFolderParserService, FolderParserService>()
                                               .AddScoped<IOutputService, ConsoleOutputService>()    
                                               .AddScoped<IFileReaderService, FileReaderService>()
                                               .AddScoped<IMedianManager, MedianManager>()
                                               .AddScoped<IFolderManager, FolderManager>()
                                               .AddScoped<IDataProcessor, DataProcessor>()
                                               .BuildServiceProvider();
            //Put config values to static property
            InitConfig();
            return _serviceProvider;
        }

        private static void InitConfig()
        {
            var _configService = _serviceProvider.GetService<IConfigService>();

            //read configuration for all valid EDI file types e.g. LP or TOU etc and other related data.
            Settings = _configService.GetSection<SourceFolderSettings>(nameof(SourceFolderSettings));           
        }

        //We are done!!
        public static void DisposeServices()
        {
            if (_serviceProvider == null) return;

            if (_serviceProvider is IDisposable) ((IDisposable)_serviceProvider).Dispose();
        }
    }
}
