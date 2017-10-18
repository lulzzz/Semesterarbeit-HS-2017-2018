using System;

namespace MathSubSet.regression
{
    class RegressionResults
    {
        private static readonly int SSE_IDX = 0;
        private static readonly int SST_IDX = 1;
        private static readonly int RSQ_IDX = 2;
        private static readonly int MSE_IDX = 3;
        private static readonly int ADJRSQ_IDX = 4;
        private static readonly long serialVersionUID = 1L;
        private readonly double[] parameters;
        private readonly double[][] varCovData;
        private readonly bool isSymmetricVCD;
        private readonly int rank;
        private readonly long nobs;
        private readonly bool containsConstant;
        private readonly double[] globalFitInfo;

        private RegressionResults()
        {
            this.parameters = null;
            this.varCovData = ((double[][])null);
            this.rank = -1;
            this.nobs = -1L;
            this.containsConstant = false;
            this.isSymmetricVCD = false;
            this.globalFitInfo = null;
        }

        public RegressionResults(double[] parameters, double[][] varcov, bool isSymmetricCompressed, long nobs, int rank, double sumy, double sumysq, double sse, bool containsConstant, bool copyData)
        {
            if (copyData)
            {
                Array.Copy(parameters, this.parameters, 0);
                this.varCovData = new double[varcov.Length][];
                for (int i = 0; i < varcov.Length; i++)
                {
                     Array.Copy(varcov[i], this.varCovData[i], i);
                }
            }
            else
            {
                this.parameters = parameters;
                this.varCovData = varcov;
            }
            this.isSymmetricVCD = isSymmetricCompressed;
            this.nobs = nobs;
            this.rank = rank;
            this.containsConstant = containsConstant;
            this.globalFitInfo = new double[5];
            Array.Fill(this.globalFitInfo, Double.NaN);
            if (rank > 0)
            {
                this.globalFitInfo[1] = (containsConstant ? sumysq - sumy * sumy / nobs : sumysq);
            }
            this.globalFitInfo[0] = sse;
            this.globalFitInfo[3] = (this.globalFitInfo[0] / (nobs - rank));

            this.globalFitInfo[2] = (1.0D - this.globalFitInfo[0] / this.globalFitInfo[1]);
            if (!containsConstant)
            {
                this.globalFitInfo[4] = (1.0D - (1.0D - this.globalFitInfo[2]) * (nobs / (nobs - rank)));
            }
            else
            {
                this.globalFitInfo[4] = (1.0D - sse * (nobs - 1.0D) / (this.globalFitInfo[1] * (nobs - rank)));
            }
        }

        public double GetParameterEstimate(int index) //throws OutOfRangeException
        {
            if (this.parameters == null)
            {
                return Double.NaN;
            }
            if ((index < 0) || (index >= this.parameters.Length))
            {
               // throw new OutOfRangeException(Integer.valueOf(index), Integer.valueOf(0), Integer.valueOf(this.parameters.length - 1));
            }
            return this.parameters[index];
        }

        public double[] GetParameterEstimates()
        {
            if (this.parameters == null)
            {
                return null;
            }
            return this.parameters;
        }

        public double GetStdErrorOfEstimate(int index) // throws OutOfRangeException
        {
            if (this.parameters == null)
            {
                return Double.NaN;
            }
            if ((index < 0) || (index >= this.parameters.Length))
            {
               // throw new OutOfRangeException(Integer.valueOf(index), Integer.valueOf(0), Integer.valueOf(this.parameters.Length - 1));
            }
            double var = GetVcVElement(index, index);
            if ((!Double.IsNaN(var)) && (var > Double.MinValue))
            {
                return Math.Sqrt(var);
            }
            return Double.NaN;
        }

        public double[] GetStdErrorOfEstimates()
        {
            if (this.parameters == null)
            {
                return null;
            }
            double[] se = new double[this.parameters.Length];
            for (int i = 0; i < this.parameters.Length; i++)
            {
                double var = GetVcVElement(i, i);
                if ((!Double.IsNaN(var)) && (var > Double.MinValue))
                {
                    se[i] = Math.Sqrt(var);
                }
                else
                {
                    se[i] = Double.NaN;
                }
            }
            return se;
        }

        public double GetCovarianceOfParameters(int i, int j) //throws OutOfRangeException
        {
            if (this.parameters == null)
            {
                return Double.NaN;
            }
            if ((i < 0) || (i >= this.parameters.Length))
            {
                //throw new OutOfRangeException(Integer.valueOf(i), Integer.valueOf(0), Integer.valueOf(this.parameters.length - 1));
            }
            if ((j < 0) || (j >= this.parameters.Length))
            {
                //throw new OutOfRangeException(Integer.valueOf(j), Integer.valueOf(0), Integer.valueOf(this.parameters.length - 1));
            }
            return GetVcVElement(i, j);
        }

        public int GetNumberOfParameters()
        {
            if (this.parameters == null)
            {
                return -1;
            }
            return this.parameters.Length;
        }

        public long GetN()
        {
            return this.nobs;
        }

        public double GetTotalSumSquares()
        {
            return this.globalFitInfo[1];
        }

        public double GetRegressionSumSquares()
        {
            return this.globalFitInfo[1] - this.globalFitInfo[0];
        }

        public double GetErrorSumSquares()
        {
            return this.globalFitInfo[0];
        }

        public double GetMeanSquareError()
        {

            return this.globalFitInfo[3];
        }

        public double GetRSquared()
        {
            return this.globalFitInfo[2];
        }

        public double GetAdjustedRSquared()
        {
            return this.globalFitInfo[4];
        }

        public bool HasIntercept()
        {
            return this.containsConstant;
        }

        private double GetVcVElement(int i, int j)
        {
            if (this.isSymmetricVCD)
            {
                if (this.varCovData.Length > 1)
                {
                    if (i == j)
                    {
                        return this.varCovData[i][i];
                    }
                    if (i >= this.varCovData[j].Length)
                    {
                        return this.varCovData[i][j];
                    }
                    return this.varCovData[j][i];
                }
                if (i > j)
                {
                    return this.varCovData[0][((i + 1) * i / 2 + j)];
                }
                return this.varCovData[0][((j + 1) * j / 2 + i)];
            }
            return this.varCovData[i][j];
        }
    }
}
