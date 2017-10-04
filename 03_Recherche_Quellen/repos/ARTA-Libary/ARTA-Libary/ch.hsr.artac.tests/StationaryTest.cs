using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.tests
{
    internal class StationaryTest
    {
        internal static bool IsStationary(double[] alphas)
        {
            int dim = alphas.Length;
            double[] a = new double[dim + 1];
            double[] b = new double[dim + 1];

            a[0] = -1.0;
            for (int i = 0; i < dim; i++)
            {
                a[i + 1] = alphas[i];
            }

            for (int k = dim; k > 0; k--)
            {
                for (int j = 0; j < k; j++)
                {
                    b[j] = a[0] * a[j] - a[k] * a[k - j];

                }
                if (b[0] <= 0.0)
                {
                    //process is not stationary
                    return false;
                }

                for (int i = 0; i < dim + 1; i++)
                {
                    a[i] = b[i];
                }
            }
            //process is stationary 
            return true;
        }
    }
}
