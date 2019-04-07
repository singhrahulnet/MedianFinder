using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MedianFinder.Models
{
    public class MedianVarianceResult
    {
        private string fileInitialPattern = @"(?<fileType>([a-zA-Z]+))[_]";
        public string DateTimeColumnName { get => "Date/Time"; }
        public string DataColumnName
        {
            get
            {
                var fileInitial = Regex.Match(FileName, fileInitialPattern);
                return Startup.Settings.FileTypes[fileInitial.Groups["fileType"].Value];
            }
        }
        public string FileName { get; set; }
        public decimal Median { get; set; }
        public List<VarianceData> VarianceData { get; set; }                
    }
    public class VarianceData
    {
        public string Date { get; set; }
        public decimal Value { get; set; }
    }
}
