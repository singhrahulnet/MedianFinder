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
            //Always good to validate the input parameter in public methods
            if (result == null) return;

            foreach (var row in result.VarianceData)
            {
                Console.WriteLine($"{result.FileName} {row.Date} {row.Value} {result.Median}");
            }
        }
    }
}
