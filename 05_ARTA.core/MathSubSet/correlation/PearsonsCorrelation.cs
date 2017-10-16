using Math3.distribution;
using Math3.linear;
using MathSubSet.regression;
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
            int nVars = this.correlationMatrix.getColumnDimension();
            double[][] outVar = new double[nVars][];
            for(int i = 0; i < nVars; i++)
            {
                for(int j = 0; j < nVars; j++)
                {
                    double r = this.correlationMatrix.getEntry(i, j);
                    outVar[i][j] = Math.Sqrt((1.0D - r * r) / (this.nObs - 2));
                }
            }
            return new BlockRealMatrix(outVar);
        }
        public RealMatrix GetCorrelationPValues()
        {
            TDistribution tDistribution = new TDistribution(this.nObs - 2);
            int nVars = this.correlationMatrix.getColumnDimension();
            double[][] outVar = new double[nVars][];
            for(int i = 0; i < nVars; i++)
            {
                for(int j = 0; j < nVars; j++)
                {
                    if(i == j)
                    {
                        outVar[i][j] = 0.0D;
                    }
                    else
                    {
                        double r = this.correlationMatrix.getEntry(i, j);
                        double t = Math.Abs(r * Math.Sqrt((this.nObs - 2) / (1.0D - r * r)));
                        outVar[i][j] = (2.0D * tDistribution.cumulativeProbability(-t));
                    }
                }
            }
            return new BlockRealMatrix(outVar);
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
                    double corr = Correlation(matrix.getColumn(i), matrix.getColumn(j));
                    outMatrix.setEntry(i, j, corr);
                    outMatrix.setEntry(j, i, corr);
                }
                outMatrix.setEntry(i, i, 1.0D);
            }
            return outMatrix;
        }
        public RealMatrix ComputeCorrelationMatrix(double[][] data)
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
                regression.AddData(xArray[i], yArray[i]);
            }
            return regression.GetR();
        }
        public RealMatrix CovarianceToCorrelation(RealMatrix covarianceMatrix)
        {
            int nVars = covarianceMatrix.getColumnDimension();
            RealMatrix outMatrix = new BlockRealMatrix(nVars, nVars);
            for(int i = 0; i < nVars; i++)
            {
                double sigma = Math.Sqrt(covarianceMatrix.getEntry(i, i));
                outMatrix.setEntry(i, i, 1.0D);
                for(int j = 0; j < i; j++)
                {
                    double entry = covarianceMatrix.getEntry(i, j) / (sigma * Math.Sqrt(covarianceMatrix.getEntry(j, j)));
                    outMatrix.setEntry(i, j, entry);
                    outMatrix.setEntry(j, i, entry);
                }
            }
            return outMatrix;
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