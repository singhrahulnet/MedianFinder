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
    public class DataProcessor : IDataProcessor
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
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException("File path is empty");

            _fileService.InitFileReader(filepath, fileDelimiter);

            var result = new MedianVarianceResult
            {
                FileName = _fileService.FileName,
                VarianceData = new List<VarianceData>()
            };

            result.Median = _calcService.GetMedian(_fileService.IterateFile(result.DataColumnName));

            foreach (var data in _fileService.IterateFile(result.DataColumnName))
            {
                //Move to next iteration as value is not a decimal
                if (!Decimal.TryParse(data, out decimal dataValue)) continue; 
                
                //Let's find out if our value is within variance range
                bool inRange = _calcService.IsValueInMedianRange(result.Median, dataValue, lowerVariancePC, upperVariancePC);

                if (inRange)
                {
                    result.VarianceData.Add(new VarianceData
                    {
                        Value = dataValue,
                        Date = _fileService.GetValueFromCurrentLine(result.DateTimeColumnName)
                    }
                    );
                }
            }

            return result;
        }
    }
}
