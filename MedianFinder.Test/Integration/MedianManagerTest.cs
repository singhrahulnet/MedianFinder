using MedianFinder.Managers;
using MedianFinder.Models;
using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
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
        IFileSystem _fileSystem = null;

        public MedianManagerTest()
        {
            _folderParserService = new FolderParserService();
            _folderManager = new FolderManager(_folderParserService);
            _calculationService = new CalculationService();
            _fileSystem = new FileSystem();
            _fileReaderService = new FileReaderService(_fileSystem);
            _dataProcessor = new DataProcessor(_calculationService, _fileReaderService);
            _outputService = new ConsoleOutputService();
        }
        public void Dispose()
        {
            _folderParserService = null;
            _folderManager = null;
            _calculationService = null;
            _fileSystem = null;
            _fileReaderService = null;
            _dataProcessor = null;
            _outputService = null;
        }
        [Fact]
        public void StartProcess_returns_number_of_file_processed()
        {
            //given
            var settings = new SourceFolderSettings()
            {
                FileFormat = new FileFormat() { Delimiter = ",", Ext = "*.csv" },
                FileTypes = new Dictionary<string, string>() {
                {"TOU","Energy" },
                {"LP","Data Value" }
            },
                Path = "D:\\sample Files",
                LowerVariancePC = 20,
                UpperVariancePC = 20
            };
            var sut = new MedianManager(_folderManager, _dataProcessor, _outputService);

            //When
            var numberofFilesProcessed = sut.StartProcess(settings);

            //Then
            Assert.IsType<int>(numberofFilesProcessed);
        }
    }
}
