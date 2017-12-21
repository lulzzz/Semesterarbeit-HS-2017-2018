using Arta;
using StatisticalTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new ArtaExecutionContext(ArtaExecutionContext.Distribution.ExponentialDistribution, new double[]{ 0.4 });
            var test = new ArtaStatistics(context).Initialize(10).Iterations(1000).Acfs().Excecute() ;
        }
    }
}
