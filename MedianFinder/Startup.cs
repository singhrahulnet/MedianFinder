using MedianFinder.Managers;
using MedianFinder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Abstractions;

namespace MedianFinder
{
    public class Startup
    {
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
                                               .AddScoped<IFileSystem, FileSystem>()
                                               .AddScoped<IMedianManager, MedianManager>()
                                               .AddScoped<IFolderManager, FolderManager>()
                                               .AddScoped<IMainManager, MainManager>()
                                               .AddScoped<IDataProcessor, DataProcessor>()
                                               .BuildServiceProvider();

            return _serviceProvider;
        }

        //We are done!!
        public static void DisposeServices()
        {
            if (_serviceProvider == null) return;

            if (_serviceProvider is IDisposable) ((IDisposable)_serviceProvider).Dispose();
        }
    }
}
