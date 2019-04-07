using MedianFinder.Models;
using MedianFinder.Services;
using System;
using System.Collections.Generic;

namespace MedianFinder.Managers
{
    public interface IDataProcessor
    {
        MedianVarianceResult GetMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC);
    }
    class DataProcessor : IDataProcessor
    {
        private readonly ICalculationService _calcService;
        private readonly IFileService _fileService;

        public DataProcessor(ICalculationService calcService, IFileService fileService)
        {
            _calcService = calcService ?? throw new ArgumentNullException(nameof(calcService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public MedianVarianceResult GetMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(fileDelimiter)) return null;

            MedianVarianceResult result = null;

            result = PopulateMedianVariance(filepath, fileDelimiter, lowerVariancePC, upperVariancePC);

            return result;
        }

        private MedianVarianceResult PopulateMedianVariance(string filepath, string fileDelimiter, decimal lowerVariancePC, decimal upperVariancePC)
        {
            //Init file reader so that headers/path/delimiter are pre-populated and saved for later usage
            _fileService.InitFileReader(filepath, fileDelimiter);

            //Init result with fileName.
            var result = new MedianVarianceResult
            {
                FileName = _fileService.FileName,
                VarianceData = new List<VarianceData>()
            };

            //Let's calculate the median. Pass in the Data Column Name from Model
            result.Median = _calcService.GetMedian(_fileService.IterateFile(result.DataColumnName));

            //Iterate each row now
            foreach (var data in _fileService.IterateFile(result.DataColumnName))
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
                        //No need to iterate over again as fileService keeps the current line saved
                        Date = _fileService.GetValueFromCurrentLine(result.DateTimeColumnName)
                    }
                    );
                }
            }

            return result;
        }
    }
}
