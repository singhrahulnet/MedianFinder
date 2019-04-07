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
        IFolderService _folderService = null;
        IFolderManager _folderManager = null;
        ICalculationService _calculationService = null;
        IFileService _fileService = null;
        IDataProcessor _dataProcessor = null;
        IOutputService _outputService = null;
        public MedianManagerTest()
        {
            _folderService = new FolderService();
            _folderManager = new FolderManager(_folderService);
            _calculationService = new CalculationService();
            _fileService = new FileService();
            _dataProcessor = new DataProcessor(_calculationService, _fileService);
            _outputService = new ConsoleOutputService();
        }
        public void Dispose()
        {
            _folderService = null;
            _folderManager = null;
            _calculationService = null;
            _fileService = null;
            _dataProcessor = null;
            _outputService = null;
        }
        [Fact]
        public void StartProcess_returns_number_of_file_processed()
        {
            //given

            var sut = new MedianManager(_folderManager, _dataProcessor, _outputService);
            Startup.ConfigureServices();

            //When
            var numberofFilesProcessed = sut.StartProcess();

            //Then
            Assert.IsType<int>(numberofFilesProcessed);

        }
    }
}
