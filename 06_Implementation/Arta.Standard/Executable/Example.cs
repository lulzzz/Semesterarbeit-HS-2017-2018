using Arta.Math;
using MathNet.Numerics.Distributions;
using StatisticalTests;
using System;
using System.IO;
using System.Text;

namespace Arta.Executable
{
    class Example
    {
        public static void Main(String[] args)
        {           
            var executionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ContinousUniformDistribution, new double[] { -0.4, 0.5 });
            var artaProcess = executionContext.CreateArtaProcess();

            //ArtaStatistics arta = new ArtaStatistics(executionContext).Initialize(10).Iterations(1000).ArtaNumbers().Acfs().Pacfs().Excecute();

            double[] arNumbers = new double[1500];
            for(int i = 0; i < arNumbers.Length; i++)
            {
                arNumbers[i] = artaProcess.GetArProcess().Next();
            }


            int lag = 10;

            double[] artaNumbers = new double[1500];
            for (int i = 0; i < artaNumbers.Length; i++)
            {
                artaNumbers[i] = artaProcess.Next();
            }

            double[] test = new double[100];
            Normal nDist = new Normal();
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = nDist.Sample();
            }

            double[] acfs = AutoCorrelation.CalculateAcfs(artaNumbers, lag);
            double[] pacfs = AutoCorrelation.CalculatePacfs(acfs);

            StringBuilder distriOut = new StringBuilder();
            var contPath = @"C:\Users\Philipp\Desktop\arNumbers.csv";
            foreach (var coeff in arNumbers)
            {
                distriOut.Append(coeff + "\n");
                

            }
            File.AppendAllText(contPath, distriOut.ToString());

            StringBuilder artaOut = new StringBuilder();
            var artaPath = @"C:\Users\Philipp\Desktop\outArtaStdCont.csv";
            foreach (var coeff in artaNumbers)
            {
                artaOut.Append(coeff + "\n");
                

            }
            File.AppendAllText(artaPath, artaOut.ToString());

            StringBuilder pacfsOut = new StringBuilder();
            var artaPacfsPath = @"C:\Users\Philipp\Desktop\PacfsARTAStd.csv";
            foreach (var coeff in pacfs)
            {
                pacfsOut.Append(coeff + "\n");
                

            }
            File.AppendAllText(artaPacfsPath, pacfsOut.ToString());


            StringBuilder acfsOut = new StringBuilder();
            var artaAcfsPath = @"C:\Users\Philipp\Desktop\AcfsARTAStd.csv";
            foreach (var coeff in acfs)
            {
                acfsOut.Append(coeff + "\n");
                

            }
            File.AppendAllText(artaAcfsPath, acfsOut.ToString());
            Console.WriteLine(nDist.Variance);

            Console.ReadKey();
        }
    }
}
