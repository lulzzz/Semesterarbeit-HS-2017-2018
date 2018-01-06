using Arta;
using Arta.Fitting;
using Arta.Math;
using System;

namespace StatisticalTests
{
    public class ArtaStatistics
    {
        private int lag;
        private int iterations;
        private int order;
        private readonly ArtaExecutionContext context;
        private double[] artaNumbers;
        private double[] pacfs;
        private double[] acfs;
        private double[] correlationCoefficients;
        private double[] arNumbers;
        private bool printOrder, printArtaNumbers, printAcfs, printPacfs, printCorrelationmatrix, printArnumbers;

        public ArtaStatistics(ArtaExecutionContext context, int lag, int iterations)
        {
            this.context = context;
            this.correlationCoefficients = context.ArtaCorrelationCoefficients;
            this.lag = lag;
            this.iterations = iterations;

        }
        public ArtaStatistics Acfs()
        {
            printAcfs = true;
            return this;
        }

        public ArtaStatistics Pacfs()
        {
            acfs = AutoCorrelation.CalculateAcfs(artaNumbers, lag); 
            printPacfs = true;
            return this;
        }

        public ArtaStatistics Order()
        {
            
            printOrder = true;
            return this;
        }

        public ArtaStatistics ArtaNumbers()
        {
            printArtaNumbers = true;
            return this;
        }

        public ArtaStatistics ArNumbers()
        {
            
            arNumbers = new double[iterations];
            for(int i = 0; i < iterations; i++)
            {
                arNumbers[i] = context.ArtaProcess.Next();
            }
            return this;
        }

        public ArtaStatistics CorrelationMatrix()
        {
            AutoCorrelation.GetCorrelationMatrix(context.ArtaCorrelationCoefficients);
            return this;
        }

        public void PrintAcfs()
        {
            acfs = AutoCorrelation.CalculateAcfs(artaNumbers, lag);
            Console.WriteLine("###############################################\n");
            Console.WriteLine("ACFS:");
            foreach (var num in acfs)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintPacfs()
        {
            pacfs = AutoCorrelation.CalculatePacfs(acfs);
            Console.WriteLine("###############################################\n");
            Console.WriteLine("PACFS:");
            foreach (var num in pacfs)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintOrder()
        {
            order = OrderEstimator.EstimateOrder(artaNumbers, lag);
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

        public void PrintArnumbers()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("AR Numbers: ");
            foreach (var num in arNumbers)
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
            int i = 1;
            foreach(var coeff in correlationCoefficients)
            {
                Console.WriteLine("Correlationcoefficients " + i + ": {0}\t\t", coeff);
            }
           
            Console.WriteLine("Lag: {0}\n", lag);
            Console.WriteLine("Iterations: {0}", iterations);
        }

        public ArtaStatistics Excecute()
        {
            artaNumbers = new double[iterations];
            for (int i = 0; i < iterations; i++)
            {
                artaNumbers[i] = context.ArtaProcess.Next();
            }
        
            PrintBasicInformation();
            if (printAcfs)
                PrintAcfs();
            if (printPacfs)
                PrintPacfs();
            if (printOrder)
                PrintOrder();
            if (printArtaNumbers)
                PrintArtanumbers();
            if (printArnumbers)
                PrintArnumbers();

            return this;
        }

        void WriteToExcel(string path)
        {
            /*
            Microsoft.Office.Interop.Excel._Workbook workbook;
            Microsoft.Office.Interop.Excel._Worksheet sheet;
            Microsoft.Office.Interop.Excel.Range range;
            object misvalue = System.Reflection.Missing.Value;

            sheet.Cells.Value = "blabla";

            sheet.Cells[1, 1] = "ArtaStatistics - Report";
            sheet.Cells[1, 2] = "Datum: " + DateTime.Now;
            */

        }
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
