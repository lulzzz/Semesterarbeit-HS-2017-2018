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
   
        }
    
        void WriteToExcel(string path)
        {

        }
    }
}
