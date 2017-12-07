using Arta.Fitting;
using Arta.Math;
using MathNet.Numerics.LinearAlgebra;
using System;
using Arta.Distribution;

namespace StatisticalTests
{
    public class ArtaStatistics
    {
        private int lag, iterations, order;
        private readonly ArtaExecutionContext context;
        private Matrix<double> corrMatrix;
        private double[] artaNumbers, pacfs, acfs, correlationCoefficients, arNumbers;
        private bool printOrder, printArtaNumbers, printAcfs, printPacfs, printCorrelationmatrix, printArnumbers;

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
            var artaProcess = context.ArtaProcess;
            artaNumbers = new double[iterations];
            for (int i = 0; i < iterations; i++)
            {
                artaNumbers[i] = artaProcess.Next();
            }
            printArtaNumbers = true;
            return this;
        }

        public ArtaStatistics ArNumbers()
        {
            var artaProcess = context.ArtaProcess;
            arNumbers = new double[iterations];
            for(int i = 0; i < iterations; i++)
            {
                arNumbers[i] = artaProcess.GetArProcess().Next();
            }
            return this;
        }

        public ArtaStatistics CorrelationMatrix()
        {

            corrMatrix = AutoCorrelation.GetCorrelationMatrix(context.ArtaCorrelationCoefficients);
            printCorrelationmatrix = true;
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

        public void PrintArnumbers()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("Arta Numbers: ");
            foreach (var num in artaNumbers)
            {
                Console.WriteLine(num);
            }
        }

        public void PrintCorrelationmatrix()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("Correlationmatrix");
            Console.WriteLine(corrMatrix.ToMatrixString());
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
            if (printArnumbers)
                PrintArnumbers();
            if (printCorrelationmatrix)
                PrintCorrelationmatrix();

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
    }
}
