using System.Collections.Generic;

namespace MedianFinder.Models
{
    public class SourceFolderSettings
    {
        public string Path { get; set; }
        public string FileDelimiter { get; set; }
        public Dictionary<string, string> FileTypes { get; set; }
        public decimal LowerVariancePC { get; set; }
        public decimal UpperVariancePC { get; set; }        
    }
}
