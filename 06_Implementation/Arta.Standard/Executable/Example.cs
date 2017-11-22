using Arta.Fitting;
using Arta.Math;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Arta.Executable
{
    class Example
    {
        public static void Main(String[] args)
        {
            double[] values = { 1, 2, 3, 2 };
            EmpiricalDistribution dist = new EmpiricalDistribution(values);

            for (double d = 0; d <= 10; d++)
            {
                Console.WriteLine(dist.InverseCumulativeProbability(d / 10));
            }
            
            var distributionType = nameof(ContinuousUniformDistribution);
            
            double[] artaCorrelationCoefficients = { 0.3, 0.3, -0.1 };
            IArtaProcess arta = ArtaProcessFactory.CreateArtaProcess(distributionType, artaCorrelationCoefficients);

            double[] data = new double[10000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = arta.Next();
            }

            int maxLag = 10;
            double[] acfs = AutoCorrelation.CalculateAcfs(data, maxLag);
            double[] pacfs = AutoCorrelation.CalculatePacfs(acfs);

            Console.WriteLine("#########################");
            Console.WriteLine("ACFS");
            foreach (double d in acfs)
            {
                Console.WriteLine(d.ToString());
            }
            Console.WriteLine('\n');

            Console.WriteLine("#########################");
            Console.WriteLine("PACFS");
            foreach (double d in pacfs)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine('\n');

            Console.WriteLine("#########################");
            Console.WriteLine("Order");
            Console.WriteLine(OrderEstimator.EstimateOrder(data, maxLag));
        }
    }
}
