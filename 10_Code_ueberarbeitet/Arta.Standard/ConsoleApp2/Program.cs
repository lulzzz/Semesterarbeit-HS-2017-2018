using System;
using Arta;
using StatisticalTests;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ArtaExecutionContext(ArtaExecutionContext.Distribution.ExponentialDistribution, new double[] { 0.4 });
            var test = new ArtaStatistics(context).Initialize(10).Iterations(1000).Acfs().Excecute();
            Console.ReadKey();
        }
    }
}
