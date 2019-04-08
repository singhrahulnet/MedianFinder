using MedianFinder.Managers;
using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MedianFinder.Test.Integration
{
    public class MedianManagerTest : IDisposable
    {
        IFolderParserService _folderParserService = null;
        IFolderManager _folderManager = null;
        ICalculationService _calculationService = null;
        IFileReaderService _fileReaderService = null;
        IDataProcessor _dataProcessor = null;
        IOutputService _outputService = null;
        public MedianManagerTest()
        {
            _folderParserService = new FolderParserService();
            _folderManager = new FolderManager(_folderParserService);
            _calculationService = new CalculationService();
            _fileReaderService = new FileReaderService();
            _dataProcessor = new DataProcessor(_calculationService, _fileReaderService);
            _outputService = new ConsoleOutputService();
            Startup.ConfigureServices();
        }
        public void Dispose()
        {
            _folderParserService = null;
            _folderManager = null;
            _calculationService = null;
            _fileReaderService = null;
            _dataProcessor = null;
            _outputService = null;
        }
        [Fact]
        public void StartProcess_returns_number_of_file_processed()
        {
            //given
            var sut = new MedianManager(_folderManager, _dataProcessor, _outputService);

            //When
            var numberofFilesProcessed = sut.StartProcess();

            //Then
            Assert.IsType<int>(numberofFilesProcessed);
        }
    }
}
