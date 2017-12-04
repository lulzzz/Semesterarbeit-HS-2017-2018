using Arta.Math;
using StatisticalTests;
using System;

namespace Arta.Executable
{
    class Example
    {
        public static void Main(String[] args)
        {           
            var executionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ContinousUniformDistribution, new double[] { -0.4, 0.5 });
            var artaProcess = executionContext.CreateArtaProcess();

            ArtaStatistics arta = new ArtaStatistics(executionContext).Initialize(10).Iterations(1000).ArtaNumbers().Acfs().Pacfs().Excecute();
          
            /*
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
            */


            Console.ReadKey();
        }
    }
}
