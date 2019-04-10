using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MedianFinder.Models
{
    public class MedianVarianceResult
    {
        public MedianVarianceResult(string fileName, List<VarianceData> varianceData, Dictionary<string, string> fileTypes)
        {
            FileName = fileName;
            VarianceData = varianceData;
            FileTypes = fileTypes;
        }
        public string FileName { get; private set; }
        public decimal Median { get; set; }
        public List<VarianceData> VarianceData { get; private set; }
        Dictionary<string, string> FileTypes;
        public string DateTimeColumnName { get => "Date/Time"; }
        public string DataColumnName
        {
            get
            {
                var fileInitial = Regex.Match(FileName, Helpers.RegexHelper.FileInitialPattern);
                return FileTypes[fileInitial.Groups[Helpers.RegexHelper.FileTypeGroupName].Value];
            }
        }
    }
    public class VarianceData
    {
        public VarianceData(string date, decimal value)
        {
            Date = date; Value = value;
        }
        public string Date { get; private set; }
        public decimal Value { get; private set; }
    }
}
