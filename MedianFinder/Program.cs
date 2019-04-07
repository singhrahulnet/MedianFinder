using MedianFinder.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace MedianFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            IMedianManager medianManager = Startup
                           .ConfigureServices()
                           .GetService<IMedianManager>();           //Setup infrastructure           

            medianManager.StartProcess();                           //Start the process

            Startup.DisposeServices();                              //Release resources

            Console.Read();                                         //Wait to verify results
        }
    }
}
