using Arta.Fitting;
using Arta.Math;
using System;

namespace StatisticalTests
{
    public class ArtaStatistics
    {
        private int lag, iterations, order;
        private readonly ArtaExecutionContext context;
        private double[] artaNumbers, pacfs, acfs, correlationCoefficients;
        private bool printOrder, printArtaNumbers, printAcfs, printPacfs;

        public ArtaStatistics(ArtaExecutionContext context)
        {
            this.context = context;
            this.correlationCoefficients = context.ArtaCorrelationCoefficients;
        }

        public ArtaStatistics Initialize(int lag)
        {
            this.lag = lag;
            return this;
        }

        public ArtaStatistics Iterations(int iterations)
        {
            this.iterations = iterations;
            return this;
        }

        public ArtaStatistics Acfs()
        {
            acfs = AutoCorrelation.CalculateAcfs(artaNumbers, lag);
            printAcfs = true;
            return this;
        }

        public ArtaStatistics Pacfs()
        {
            pacfs = AutoCorrelation.CalculatePacfs(acfs);
            printPacfs = true;
            return this;
        }

        public ArtaStatistics Order()
        {
            order = OrderEstimator.EstimateOrder(artaNumbers, lag);
            printOrder = true;
            return this;
        }

        public ArtaStatistics ArtaNumbers()
        {
            var artaProcess = context.CreateArtaProcess();
            artaNumbers = new double[iterations];
            for (int i = 0; i < iterations; i++)
            {
                artaNumbers[i] = artaProcess.Next();
            }
            printArtaNumbers = true;
            return this;
        }

        public void PrintAcfs()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("ACFS:");
            foreach (var num in acfs)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintPacfs()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("PACFS:");
            foreach (var num in pacfs)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintOrder()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("Order: {0}", order);
        }

        public void PrintArtanumbers()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("Arta Numbers: ");
            foreach (var num in artaNumbers)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintBasicInformation()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("ARTA-Statistics\n");
            Console.WriteLine("Inputvalues:\n");
            Console.WriteLine("Distribution: {0}\n", context.distribution.ToString());
            Console.WriteLine("Correlationcoefficients: {0}, {1}\n", correlationCoefficients[0], correlationCoefficients[1]);
            Console.WriteLine("Lag: {0}\n", lag);
            Console.WriteLine("Iterations: {0}", iterations);
        }

        public ArtaStatistics Excecute()
        {
            PrintBasicInformation();
            if (printAcfs)
                PrintAcfs();
            if (printPacfs)
                PrintPacfs();
            if (printOrder)
                PrintOrder();
            if (printArtaNumbers)
                PrintArtanumbers();

            return this;
        }

        void WriteToExcel(string path)
        {
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
        }
    }
}
