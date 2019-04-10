using MedianFinder.Managers;
using MedianFinder.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.IO.Abstractions;
using Xunit;

namespace MedianFinder.Test.Integration
{
    public class AppLauncherTest : IDisposable
    {
        IFolderParserService _folderParserService = null;
        IFolderManager _folderManager = null;
        ICalculationService _calculationService = null;
        IFileReaderService _fileReaderService = null;
        IDataProcessor _dataProcessor = null;
        IOutputService _outputService = null;
        IFileSystem _fileSystem = null;
        IMedianManager _medianManager = null;
        IConfigService _configService = null;
        public AppLauncherTest()
        {
            _fileSystem = new FileSystem();
            _folderParserService = new FolderParserService(_fileSystem);
            _folderManager = new FolderManager(_folderParserService);
            _calculationService = new CalculationService();
            _fileReaderService = new FileReaderService(_fileSystem);
            _dataProcessor = new DataProcessor(_calculationService, _fileReaderService);
            _outputService = new ConsoleOutputService();
            _medianManager = new MedianManager(_folderManager, _dataProcessor, _outputService);
            _configService = new ConfigService(new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build());
        }
        public void Dispose()
        {
            _folderParserService = null;
            _folderManager = null;
            _calculationService = null;
            _fileReaderService = null;
            _dataProcessor = null;
            _outputService = null;
            _fileSystem = null;
            _medianManager = null;
            _configService = null;
        }
        [Fact]
        public void StartProcess_returns_number_of_file_processed()
        {
            //given
            var sut = new AppLauncher(_configService, _medianManager);

            //When
            var numberofFilesProcessed = sut.Launch();

            //Then
            Assert.IsType<int>(numberofFilesProcessed);
        }
    }
}
