using MedianFinder.Models;
using System;

namespace MedianFinder.Services
{
    public interface IOutputService
    {
        void OutputResult(MedianVarianceResult result);
    }

    public class ConsoleOutputService : IOutputService
    {
        public void OutputResult(MedianVarianceResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            foreach (var row in result.VarianceData)
            {
                Console.WriteLine($"{result.FileName} {row.Date} {row.Value} {result.Median}");
            }
        }
    }
}
