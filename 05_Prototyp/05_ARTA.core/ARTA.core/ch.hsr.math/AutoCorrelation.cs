using Math3.linear;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.math
{
    class AutoCorrelation
    {
        private AutoCorrelation()
        {
            // no instantiation
        }

        private readonly static PearsonsCorrelation pearsonsAc = new PearsonsCorrelation();

        public static double CalculateAcf(double[] data, int lag)
        {
            double acc = 0.0;
            int l = data.Length;
            if (lag < 0)
                throw new IllegalArgumentException("Negative lags are not allowed");
            if (lag > l)
                throw new IllegalArgumentException("Lag exceeds sample size");

            if (lag == 0)
            {
                acc = 1.0;
            }
            else
            {
                // TODO consider faster implementation without array-copying
                acc = pearsonsAc.correlation(Array.Copy(data, 0, l - lag), Array.ConstrainedCopy(data, lag, l));
            }
            return acc;
        }

        public static double[] CalculateAcfs(double[] data, int maxLag)
        {
            double[] accs = new double[maxLag + 1];
            for (int lag = 0; lag <= maxLag; lag++)
            {
                accs[lag] = CalculateAcf(data, lag);
            }
            return accs;
        }


        /**
         * 
         * @param autocorrelations
         * @return
         */
        public static RealMatrix GetCorrelationMatrix(double[] autocorrelations)
        {
            Math3.linear.RealMatrix result = null;
            int dim = autocorrelations.Length;
            double[][] psiValues = new double[dim][];

            for (int row = 0; row < dim; row++)
            {
                for (int col = row; col < dim; col++)
                {
                    if (row == col)
                    {
                        psiValues[col][row] = 1.0;
                    }
                    else
                    {
                        psiValues[col][row] = autocorrelations[col - row - 1];
                        psiValues[row][col] = autocorrelations[col - row - 1];
                    }

                }
            }

            result = new Array2DRowRealMatrix(psiValues);
            return result;
        }


        public static double[] CalculatePacfs(double[] acfs)
        {
            int size = acfs.Length;
            double[] pacfs = new double[size];
            double[,] A = new double[size, size];
            double[] V = new double[size];
            A[0, 0] = acfs[0];
            A[1, 1] = acfs[1] / acfs[0];
            V[1] = A[1, 1] / acfs[1];
            for (int k = 2; k < size; k++)
            {
                double sum = 0;
                for (int j = 1; j < k; j++)
                {
                    sum += A[j, j] * acfs[k - j];
                }
                A[k, k] = (acfs[k] - sum) / V[k - 1];

                for (int j = 1; j < k; j++)
                {
                    A[j, k] = A[j, k - 1] - A[k, k] * A[k - j, k - 1];
                }
                V[k] = V[k - 1] * (1 - A[k, k] * A[k, k]);

            }

            for (int i = 0; i < size; i++)
            {
                pacfs[i] = A[i, i];
            }
            return pacfs;
        }
    }
}
