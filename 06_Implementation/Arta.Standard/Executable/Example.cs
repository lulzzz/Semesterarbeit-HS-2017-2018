using Arta.Fitting;
using Arta.Math;
using MathNet.Numerics.Distributions;
using System;
using System.IO;
using System.Text;

namespace Arta.Executable
{
    class Example
    {
        public static void Main(String[] args)
        {      
      
            var distributionType = new Context(new ContinuousUniformDistribution());
            distributionType.Request();

            double[] artaCorrelationCoefficients = { -0.4 , 0.5 };
            IArtaProcess arta = ArtaProcessFactory.CreateArtaProcess(distributionType.State, artaCorrelationCoefficients);

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

            Console.WriteLine(distributionType.State.GetMean());


            double[] test = new double[100];
            ContinuousUniform nDist = new ContinuousUniform(-1, 1);
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = nDist.Sample();
            }

            StringBuilder distriOut = new StringBuilder();
            var contPath = @"C:\Users\Philipp\Desktop\DistriARTA.csv";
            foreach (var coeff in test)
            {
                distriOut.Append(coeff + "\n");
                File.AppendAllText(contPath, distriOut.ToString());

            }

            StringBuilder artaOut = new StringBuilder();
            var artaPath = @"C:\Users\Philipp\Desktop\outArtaStdCont.csv";
            foreach (var coeff in data)
            {
                artaOut.Append(coeff + "\n");
                File.AppendAllText(artaPath, artaOut.ToString());

            }

            StringBuilder pacfsOut = new StringBuilder();
            var artaPacfsPath = @"C:\Users\Philipp\Desktop\PacfsARTAStd.csv";
            foreach (var coeff in pacfs)
            {
                pacfsOut.Append(coeff + "\n");
                File.AppendAllText(artaPacfsPath, pacfsOut.ToString());

            }
           

            StringBuilder acfsOut = new StringBuilder();
            var artaAcfsPath = @"C:\Users\Philipp\Desktop\AcfsARTAStd.csv";
            foreach (var coeff in acfs)
            {
                acfsOut.Append(coeff + "\n");
                File.AppendAllText(artaAcfsPath, acfsOut.ToString());

            }
            Console.ReadKey();
        }
    }
}
