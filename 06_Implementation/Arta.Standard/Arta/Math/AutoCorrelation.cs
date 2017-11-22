using Arta.Util;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Math
{
    public class AutoCorrelation
    {
        private AutoCorrelation() { }

        public static double CalculateAcf(double[] data, int lag)
        {
            var acc = 0.0;
            var length = data.Length;
            if (lag < 0)
            {
                throw new ArgumentException("Negative Lags are not allowed");
            }
            if (lag > length)
            {
                throw new ArgumentException("Lag exceeds sample size");
            }
            if (lag == 0)
            {
                acc = 1.0;
            }
            else
            {
                acc = Correlation.Pearson(data.CopyOfRange(0, length - lag), data.CopyOfRange(lag, length));
            }
            return acc;
        }

        public static double[] CalculateAcfs(double[] data, int maxLag)
        {
            double[] accs = new double[maxLag + 1];
            for (var lag = 0; lag <= maxLag; lag++)
            {
                accs[lag] = CalculateAcf(data, lag);
            }
            return accs;
        }

        public static Matrix<double> GetCorrelationMatrix(double[] autocorrelations)
        {
            Matrix<double> result = null;
            var dim = autocorrelations.Length;
            double[,] psivalues = new double[dim, dim];
            for (int row = 0; row < dim; row++)
            {
                for (var col = row; col < dim; col++)
                {
                    if (row == col)
                    {
                        psivalues[col, row] = 1.0;
                    }
                    else
                    {
                        psivalues[col, row] = autocorrelations[col - row - 1];
                        psivalues[row, col] = autocorrelations[col - row - 1];
                    }
                }
            }
            result = CreateMatrix.DenseOfArray<double>(psivalues);
            return result;
        }

        public static double[] CalculatePacfs(double[] acfs)
        {
            var size = acfs.Length;
            double[] pacfs = new double[size];
            double[,] A = new double[size, size];
            double[] V = new double[size];
            A[0,0] = acfs[0];
            A[1,1] = acfs[1] / acfs[0];
            V[1] = A[1,1] / acfs[1];
            for (var k = 2; k < size; k++)
            {
                double sum = 0;
                for (var j = 1; j < k; j++)
                {
                    sum += A[j,j] * acfs[k - j];
                }
                A[k,k] = (acfs[k] - sum) / V[k - 1];
                for (var j = 1; j < k; j++)
                {
                    A[j,k] = A[j,k] - A[k,k] * A[k - j,k - 1];
                }

                V[k] = V[k - 1] * (1 - A[k,k] * A[k,k]);
            }
            for (var i = 0; i < size; i++)
            {
                pacfs[i] = A[i,i];
            }
            return pacfs;
        }
    }
}
