using MedianFinder.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace MedianFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            IMainManager mainManager = Startup
                           .ConfigureServices()
                           .GetService<IMainManager>();             //Setup infrastructure           


            mainManager.StartProcess();                             //Start the process

            Startup.DisposeServices();                              //Release resources

            Console.Read();                                         //Wait to verify resultss
        }
    }
}
