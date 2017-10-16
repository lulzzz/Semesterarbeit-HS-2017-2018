using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet
{
    class PearsonsCorrelation
    {
        private readonly RealMatrix correlationMatrix;
        private readonly int nObs;

        public PearsonsCorrelation()
        {
            this.correlationMatrix = null;
            this.nObs = 0;
        }
        public PearsonsCorrelation(double[][] data)
        {
            this(new BlockRealMatrix(data));
        }

        public PearsonsCorrelation(RealMatrix matrix)
        {
            this.nObs = matrix.getRowDimension();
            this.correlationMatrix = computeCorrelationMatrix(matrix);
        }

        public PearsonsCorrelation(Covariance covariance)
        {
            RealMatrix covarianceMatrix = covariance.getCovarianceMatrix();
            if (covarianceMatrix == null)
            {
                //throw new NullArgumentException(LocalizedFormats.COVARIANCE_MATRIX, new Object[0]);
            }
            this.nObs = covariance.getN();
            this.correlationMatrix = CovarianceToCorrelation(covarianceMatrix);
        }

        public PearsonsCorrelation(RealMatrix covarianceMatrix, int numberOfObservations)
        {
            this.nObs = numberOfObservations;
            this.correlationMatrix = CovarianceToCorrelation(covarianceMatrix);
        }

        public RealMatrix GetCorrelationMatrix()
        {
            return this.correlationMatrix;
        }
        public RealMatrix GetCorrelationStandardErrors()
        {

        }
        public RealMatrix getCorrelationPValues()
        {
            TDistribution tDistribution = new TDistribution(this.nObs - 2);
            int nVars = this.correlationMatrix.getColumnDimension();
            double[][] out = new double[nVars][];
            for(int i = 0; i < nVars; i++)
            {
                for(int j = 0; j < nVars; j++)
                {
                    if(i == j)
                    {
                        out[i][j] = 0.0D;
                    }
                }
            }
        }
        public RealMatrix ComputeCorrelationMatrix(RealMatrix matrix)
        {
            CheckSufficientData(matrix);
            int nVars = matrix.getColumnDimension();
            RealMatrix outMatrix = new BlockRealMatrix(nVars, nVars);
            for(int i = 0; i < nVars; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    double corr = correlation(matrix.getColumn(i), matrix.getColumn(j));
                    outMatrix.setEntry(i, j, corr);
                    outMatrix.setEntry(j, i, corr);
                }
                outMatrix.setEntry(i, i, 1.0D);
            }
            return outMatrix;
        }
        public RealMatrix ComputeCorrelationMatrix(double[,] data)
        {
            return ComputeCorrelationMatrix(RealMatrix matrix);
        }
        public double Correlation(double[] xArray, double[] yArray)
        {
            SimpleRegression regression = new SimpleRegression();
            if(xArray.Length != yArray.Length)
            {
                //TODO throw new DimensionMismatchException(xArray.length, yArray.length);
            }
            if(xArray.Length < 2)
            {
                //TODO throw new MathIllegalArgumentException(LocalizedFormats.INSUFFICIENT_DIMENSION, new Object[] { Integer.valueOf(xArray.length), Integer.valueOf(2) });
            }
            for(int i = 0; i < xArray.Length; i++)
            {
                regression.addData(xArray[i], yArray[i]);
            }
            return regression.getR();
        }
        public RealMatrix CovarianceToCorrelation(RealMatrix covarianceMatrix)
        {
            
        }
        private void CheckSufficientData(RealMatrix matrix)
        {
            int nRows = matrix.getRowDimension();
            int nCols = matrix.getColumnDimension();
            if((nRows < 2) || (nCols < 2))
            {
                //TODO Exception MathIllegalArgumentException(LocalizedFormats.INSUFFICIENT_ROWS_AND_COLUMNS, new Object[] { Integer.valueOf(nRows), Integer.valueOf(nCols) });
            }
        }
    }
}