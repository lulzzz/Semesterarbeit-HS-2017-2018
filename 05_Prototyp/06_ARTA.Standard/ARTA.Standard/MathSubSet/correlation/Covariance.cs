using Math3.linear;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    public class Covariance
    {
        private readonly RealMatrix covarianceMatrix;
        private readonly int n;

        public Covariance()
        {
            this.covarianceMatrix = null;
            this.n = 0;
        }

        public Covariance(double[][] data, bool biasCorrected)//throws MathIllegalArgumentException, NotStrictlyPositiveException
        {
            this.covarianceMatrix = new BlockRealMatrix(data); //, biasCorrected;
        }
        public Covariance(double[][] data) //throws MathIllegalArgumentException, NotStrictlyPositiveException
        {
            //this(data, true);
        }

        public Covariance(RealMatrix matrix, bool biasCorrected) // throws MathIllegalArgumentException
        {
            CheckSufficientData(matrix);
            this.n = matrix.getRowDimension();
            this.covarianceMatrix = ComputeCovarianceMatrix(matrix, biasCorrected);
        }

        public Covariance(RealMatrix matrix)// throws MathIllegalArgumentException
        {
            this.covarianceMatrix = matrix;
        }

        public RealMatrix GetCovarianceMatrix()
        {
            return this.covarianceMatrix;
        }

        public int getN()
        {
            return this.n;
        }

        protected RealMatrix ComputeCovarianceMatrix(RealMatrix matrix, bool biasCorrected) //throws MathIllegalArgumentException
        {
            int dimension = matrix.getColumnDimension();
            Variance variance = new Variance(biasCorrected);
            RealMatrix outMatrix = new BlockRealMatrix(dimension, dimension);
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    double cov = GetCovarianve(matrix.getColumn(i), matrix.getColumn(j), biasCorrected);
                    outMatrix.setEntry(i, j, cov);
                    outMatrix.setEntry(j, i, cov);
                }
                outMatrix.setEntry(i, i, variance.Evaluate(matrix.getColumn(i)));
            }
            return outMatrix;
        }

        protected RealMatrix ComputeCovarianceMatrix(RealMatrix matrix) // throws MathIllegalArgumentException
        {
            return ComputeCovarianceMatrix(matrix, true);
        }

        protected RealMatrix ComputeCovarianceMatrix(double[][] data, bool biasCorrected) // throws MathIllegalArgumentException, NotStrictlyPositiveException
        {
            return ComputeCovarianceMatrix(new BlockRealMatrix(data), biasCorrected);
        }

        protected RealMatrix ComputeCovarianceMatrix(double[][] data) // throws MathIllegalArgumentException, NotStrictlyPositiveException
        {
            return ComputeCovarianceMatrix(data, true);
        }

        public double GetCovarianve(double[] xArray, double[] yArray, bool biasCorrected) //throws MathIllegalArgumentException
        {
            Mean mean = new Mean();
            double result = 0.0D;
            int length = xArray.Length;
            if (length != yArray.Length)
            {
                // throw new MathIllegalArgumentException(LocalizedFormats.DIMENSIONS_MISMATCH_SIMPLE, new Object[] { Integer.valueOf(length), Integer.valueOf(yArray.length)
            }

            if (length < 2)
            {
                //throw new MathIllegalArgumentException(LocalizedFormats.INSUFFICIENT_OBSERVED_POINTS_IN_SAMPLE, new Object[] { Integer.valueOf(length), Integer.valueOf(2) });
            }
            double xMean = mean.Evaluate(xArray);
            double yMean = mean.Evaluate(yArray);
            for (int i = 0; i < length; i++)
            {
                double xDev = xArray[i] - xMean;
                double yDev = yArray[i] - yMean;
                result += (xDev * yDev - result) / (i + 1);
            }
            return biasCorrected ? result * (length / (length - 1)) : result;
        }

        public double covariance(double[] xArray, double[] yArray) // throws MathIllegalArgumentException
        {
            return GetCovarianve(xArray, yArray, true);
        }

        private void CheckSufficientData(RealMatrix matrix) //throws MathIllegalArgumentException
        {
            int nRows = matrix.getRowDimension();
            int nCols = matrix.getColumnDimension();
            if ((nRows < 2) || (nCols < 1))
            {
               // throw new MathIllegalArgumentException(LocalizedFormats.INSUFFICIENT_ROWS_AND_COLUMNS, new Object[] { Integer.valueOf(nRows), Integer.valueOf(nCols)
            }
        }
    }
}
