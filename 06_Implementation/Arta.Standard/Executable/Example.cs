using Arta.Fitting;
using Arta.Math;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Arta.Executable
{
    class Example
    {
        public static void Main(String[] args)
        {      
            //var distributionType = nameof(ContinuousUniformDistribution);
            var distributionType = new Context(new NormalDistribution());
            distributionType.Request();
            distributionType.State.Rate = 0.1;
            double[] artaCorrelationCoefficients = { 0.5 };
            IArtaProcess arta = ArtaProcessFactory.CreateArtaProcess(distributionType.State, artaCorrelationCoefficients);

            double[] data = new double[100];
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


            double[] test = new double[100];
            Normal nDist = new Normal();
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = nDist.Sample();
            }

            StringBuilder contOut = new StringBuilder();
            var contPath = @"C:\Users\Philipp\Desktop\NormalDist.csv";
            foreach (var coeff in test)
            {
                contOut.Append(coeff + "\n");
                File.AppendAllText(contPath, contOut.ToString());

            }

            StringBuilder artaOut = new StringBuilder();
            var artaPath = @"C:\Users\Philipp\Desktop\ArtaNormal.csv";
            foreach (var coeff in acfs)
            {
                artaOut.Append(coeff + "\n");
                File.AppendAllText(artaPath, artaOut.ToString());

            }
            Console.ReadKey();
        }
    }
}
