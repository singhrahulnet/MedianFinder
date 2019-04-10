using Microsoft.Extensions.DependencyInjection;
using System;


namespace MedianFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            IAppLauncher appLauncher = Startup
                           .ConfigureServices()
                           .GetService<IAppLauncher>();             //Setup infrastructure           


            appLauncher.Launch();                             //Start the process

            Startup.DisposeServices();                              //Release resources

            Console.Read();                                         //Wait to verify results
        }
    }
}
