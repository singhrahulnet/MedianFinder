using MedianFinder.Models;
using MedianFinder.Services;
using System;
using System.Collections.Generic;

namespace MedianFinder.Managers
{
    public interface IDataProcessor
    {
        MedianVarianceResult GetMedianVariance(string filePath, SourceFolderSettings settings);
    }
    class DataProcessor : IDataProcessor
    {
        private readonly ICalculationService _calcService;
        private readonly IFileReaderService _fileReaderService;

        public DataProcessor(ICalculationService calcService, IFileReaderService fileReaderService)
        {
            _calcService = calcService ?? throw new ArgumentNullException(nameof(calcService));
            _fileReaderService = fileReaderService ?? throw new ArgumentNullException(nameof(fileReaderService));
        }

        public MedianVarianceResult GetMedianVariance(string filePath, SourceFolderSettings settings)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(filePath) || settings == null) return null;


            var result = PopulateMedianVariance(filePath, settings);

            return result;
        }

        private MedianVarianceResult PopulateMedianVariance(string filePath, SourceFolderSettings settings)
        {
            //Init file reader so that headers/path/delimiter are pre-populated and saved for later usage
            _fileReaderService.InitFileReader(filePath, settings.FileFormat.Delimiter);

            var result = new MedianVarianceResult
                (
                    _fileReaderService.FileName,
                     new List<VarianceData>(),
                     settings.FileTypes
                );


            //Let's calculate the median. Pass in the Data Column Name from Model
            result.Median = _calcService.GetMedian(_fileReaderService.IterateFileOnColumn(result.DataColumnName));

            //Iterate each row now
            foreach (var data in _fileReaderService.IterateFileOnColumn(result.DataColumnName))
            {
                //Move to next iteration as value is not a decimal
                if (!decimal.TryParse(data, out decimal dataValue)) continue;

                //Let's find out if our value is within variance range
                bool inRange = _calcService.IsValueInMedianRange(result.Median, dataValue, settings.LowerVariancePC, settings.UpperVariancePC);

                if (inRange)
                {
                    //Populate the date from the line being read
                    //No need to iterate over again as FileReaderService keeps the current line saved
                    var date = _fileReaderService.GetColumnValueFromCurrentLine(result.DateTimeColumnName);
                    result.VarianceData.Add(new VarianceData(date, dataValue));
                }
            }

            return result;
        }
    }
}
