using MedianFinder.Models;
using MedianFinder.Services;
using System;
using System.Collections.Generic;

namespace MedianFinder.Managers
{
    public interface IDataProcessor
    {
        MedianVarianceResult GetMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC, Dictionary<string, string> fileTypes);
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

        public MedianVarianceResult GetMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC, Dictionary<string, string> fileTypes)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(fileDelimiter)) return null;

            MedianVarianceResult result = null;

            result = PopulateMedianVariance(filepath, fileDelimiter, lowerVariancePC, upperVariancePC, fileTypes);

            return result;
        }

        private MedianVarianceResult PopulateMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC, Dictionary<string, string> fileTypes)
        {
            //Init file reader so that headers/path/delimiter are pre-populated and saved for later usage
            _fileReaderService.InitFileReader(filepath, fileDelimiter);

            //Init result with fileName.
            var result = new MedianVarianceResult(fileTypes)
            {
                FileName = _fileReaderService.FileName,
                VarianceData = new List<VarianceData>()
            };

            //Let's calculate the median. Pass in the Data Column Name from Model
            result.Median = _calcService.GetMedian(_fileReaderService.IterateFileOnColumn(result.DataColumnName));

            //Iterate each row now
            foreach (var data in _fileReaderService.IterateFileOnColumn(result.DataColumnName))
            {
                //Move to next iteration as value is not a decimal
                if (!decimal.TryParse(data, out decimal dataValue)) continue;

                //Let's find out if our value is within variance range
                bool inRange = _calcService.IsValueInMedianRange(result.Median, dataValue, lowerVariancePC, upperVariancePC);

                if (inRange)
                {
                    result.VarianceData.Add(new VarianceData
                    {
                        Value = dataValue,

                        //Populate the date from the line being read
                        //No need to iterate over again as FileReaderService keeps the current line saved
                        Date = _fileReaderService.GetColumnValueFromCurrentLine(result.DateTimeColumnName)
                    }
                    );
                }
            }

            return result;
        }
    }
}
