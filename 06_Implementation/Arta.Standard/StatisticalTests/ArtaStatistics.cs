using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticalTests
{
    class ArtaStatistics
    {/*
        public ArtaStatistics(AppContext constext, double[] correlationCoefficients, int lag)
        {

        }
*/
        int GetLag()
        {
            return 0;
        }

        double[] GetAcfs()
        {
            return null;
        }

        double[] GetPacfs()
        {
            return null;
        }

        double GetOrder()
        {
            return 0.0;
        }

        double[] GetArtaNumbers(int size)
        {
            return null;
        }

        double[] GetDistributionNumbers(int size)
        {
            return null;
        }

        bool CheckDistributionBoundaries()
        {
            return false;
        }
        
        void PrintToConsole()
        {
            Console.WriteLine("###############################################\n");
            Console.WriteLine("ARTA-Statistics\n");
            Console.WriteLine("Inputvalues:\n");
            Console.WriteLine("Distribution: {0}\n" /*Add Context*/);
            Console.WriteLine("Correlationcoefficients: {0}, {1}\n" /*Add Corrcoeff*/);
            Console.WriteLine("Lag: {0}\n" /*Add lag*/);

            Console.WriteLine("###############################################\n");
            Console.WriteLine("ACFS:");
            for(var num in acfs)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine("###############################################\n");
            Console.WriteLine("PACFS:");
            for(var num in pacfs)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine("###############################################\n");
            Console.WriteLine("Order: {0}" /*Add Order*/);
        }
    
        void WriteToExcel(string path)
        {

        }
    }
}
