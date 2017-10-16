using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.regression
{
    class SimpleRegression : UpdatingMultipleLinearRegression
    {
        private double sumX = 0.0D;
        private double sumXX = 0.0D;
        private double sumY = 0.0D;
        private double sumYY = 0.0D;
        private double sumXY = 0.0D;
        private long n = 0L;
        private double xbar = 0.0D;
        private double ybar = 0.0D;
        private readonly bool hasIntercept;

        public SimpleRegression()
        {
            this(true);
        }
        public SimpleRegression(bool includeIntercept)
        {
            this.hasIntercept = includeIntercept;
        }
        public void AddData(double x, double y)
        {
            if (this.n == 0L)
            {
                this.xbar = x;
                this.ybar = y;
            }
            else if (this.hasIntercept)
            {
                double fact1 = 1.0D + this.n;
                double fact2 = this.n / (1.0D + this.n);
                double dx = x - this.xbar;
                double dy = y - this.ybar;
                this.sumXX += dx * dx * fact2;
                this.sumYY += dy * dy * fact2;
                this.sumXY += dx * dy * fact2;
                this.xbar += dx / fact1;
                this.ybar += dy / fact1;
            }
            if (!this.hasIntercept)
            {
                this.sumXX += x * x;
                this.sumYY += y * y;
                this.sumXY += x * y;
            }
            this.sumX += x;
            this.sumY += y;
            this.n += 1L;
        }
        public void Append(SimpleRegression reg)
        {
            if (this.n == 0L)
            {
                this.xbar = reg.xbar;
                this.ybar = reg.ybar;
                this.sumXX = reg.sumXX;
                this.sumYY = reg.sumYY;
                this.sumXY = reg.sumXY;
            }
            else if (this.hasIntercept)
            {
                double fact1 = reg.n / (reg.n + this.n);
                double fact2 = this.n * reg.n / (reg.n + this.n);
                double dx = reg.xbar - this.xbar;
                double dy = reg.ybar - this.ybar;
                this.sumXX += reg.sumXX + dx * dx * fact2;
                this.sumYY += reg.sumYY + dy * dy * fact2;
                this.sumXY += reg.sumXY + dx * dy * fact2;
                this.xbar += dx * fact1;
                this.ybar += dy * fact1;
            }
            else
            {
                this.sumXX += reg.sumXX;
                this.sumYY += reg.sumYY;
                this.sumXY += reg.sumXY;
            }
            this.sumX += reg.sumX;
            this.sumY += reg.sumY;
            this.n += reg.n;
        }
        public void RemoveData(double x, double y)
        {
            if (this.n > 0L)
            {
                if (this.hasIntercept)
                {
                    double fact1 = this.n - 1.0D;
                    double fact2 = this.n / (this.n - 1.0D);
                    double dx = x - this.xbar;
                    double dy = y - this.ybar;
                    this.sumXX -= dx * dx * fact2;
                    this.sumYY -= dy * dy * fact2;
                    this.sumXY -= dx * dy * fact2;
                    this.xbar -= dx / fact1;
                    this.ybar -= dy / fact1;
                }
                else
                {
                    double fact1 = this.n - 1.0D;
                    this.sumXX -= x * x;
                    this.sumYY -= y * y;
                    this.sumXY -= x * y;
                    this.xbar -= x / fact1;
                    this.ybar -= y / fact1;
                }
                this.sumX -= x;
                this.sumY -= y;
                this.n -= 1L;
            }
        }
        public void AddData(double[][] data)
        {
            //throws ModelSpecificationException
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length < 2)
                {
                   // throw new ModelSpecificationException(LocalizedFormats.INVALID_REGRESSION_OBSERVATION, new Object[] { Integer.valueOf(data[i].Length), Integer.valueOf(2) });
                }
                AddData(data[i][0], data[i][1]);
            }
        }
        public void AddObservation(double[] paramArrayOfDouble, double paramDouble)
        {
            if ((paramDouble == 0.0) || (paramArrayOfDouble.Length == 0))
            {
                //throw new ModelSpecificationException(LocalizedFormats.INVALID_REGRESSION_OBSERVATION, new Object[] { Integer.valueOf(x != null ? x.Length : 0), Integer.valueOf(1) });
            }
            AddData(paramArrayOfDouble[0], paramDouble);
        }

        public void AddObservations(double[][] paramArrayOfDouble, double[] paramArrayOfDouble1)
        {
            if ((paramArrayOfDouble == null) || (paramArrayOfDouble1 == null) || (paramArrayOfDouble.Length != paramArrayOfDouble1.Length))
            {
                //throw new ModelSpecificationException(LocalizedFormats.DIMENSIONS_MISMATCH_SIMPLE, new Object[] { Integer.valueOf(x == null ? 0 : x.Length), Integer.valueOf(y == null ? 0 : y.Length) });
            }
            bool obsOk = true;
            for (int i = 0; i < paramArrayOfDouble.Length; i++)
            {
                if ((paramArrayOfDouble[i] == null) || (paramArrayOfDouble[i].Length == 0))
                {
                    obsOk = false;
                }
            }
            if (!obsOk)
            {
                //throw new ModelSpecificationException(LocalizedFormats.NOT_ENOUGH_DATA_FOR_NUMBER_OF_PREDICTORS, new Object[] { Integer.valueOf(0), Integer.valueOf(1) });
            }
            for (int i = 0; i < paramArrayOfDouble.Length; i++)
            {
                AddData(paramArrayOfDouble[i][0], paramArrayOfDouble1[i]);
            }
        }
        public void RemoveData(double[][] data)
        {
            for (int i = 0; (i < data.Length) && (this.n > 0L); i++)
            {
                RemoveData(data[i][0], data[i][1]);
            }
        }
        public void Clear()
        {
            this.sumX = 0.0D;
            this.sumXX = 0.0D;
            this.sumY = 0.0D;
            this.sumYY = 0.0D;
            this.sumXY = 0.0D;
            this.n = 0L;
        }

        public long GetN()
        {
            return this.n;
        }

        public double GetIntercept()
        {
            return this.hasIntercept ? GetIntercept(GetSlope()) : 0.0D;
        }
        public bool HasIntercept()
        {
            return this.hasIntercept;
        }
        public double GetSlope()
        {
            if (this.n < 2L)
            {
                return Double.NaN;
            }
            if (Math.Abs(this.sumXX) < 4.9E-323D)
            {
                return Double.NaN;
            }
            return this.sumXY / this.sumXX;
        }
        public double Predict(double x)
        {
            double b1 = GetSlope();
            if (this.hasIntercept)
            {
                return GetIntercept(b1) + b1 * x;
            }
            return b1 * x;
        }

        public double GetSumSquaredErrors()
        {
            return Math.Max(0.0D, this.sumYY - this.sumXY * this.sumXY / this.sumXX);
        }

        public double GetTotalSumSquares()
        {
            if (this.n < 2L)
            {
                return Double.NaN;
            }
            return this.sumYY;
        }

        public double GetXSumSquares()
        {
            if (this.n < 2L)
            {
                return Double.NaN;
            }
            return this.sumXX;
        }

        public double GetSumOfCrossProducts()
        {
            return this.sumXY;
        }

        public double GetRegressionSumSquares()
        {
            return GetRegressionSumSquares(GetSlope());
        }

        public double GetMeanSquareError()
        {
            if (this.n < 3L)
            {
                return Double.NaN;
            }
            return this.hasIntercept ? GetSumSquaredErrors() / (this.n - 2L) : GetSumSquaredErrors() / (this.n - 1L);
        }

        public double GetR()
        {
            double b1 = GetSlope();
            double result = Math.Sqrt(GetRSquare());
            if (b1 < 0.0D)
            {
                result = -result;
            }
            return result;
        }

        public double GetRSquare()
        {
            double ssto = GetTotalSumSquares();
            return (ssto - GetSumSquaredErrors()) / ssto;
        }

        public double GetInterceptStdErr()
        {
            if (!this.hasIntercept)
            {
                return Double.NaN;
            }
            return Math.Sqrt(GetMeanSquareError() * (1.0D / this.n + this.xbar * this.xbar / this.sumXX));
        }

        public double GetSlopeStdErr()
        {
            return Math.Sqrt(GetMeanSquareError() / this.sumXX);
        }

        public double GetSlopeConfidenceInterval()
        {
            return GetSlopeConfidenceInterval(0.05D);
        }

        public double GetSlopeConfidenceInterval(double alpha)
        {
            if (this.n < 3L)
            {
                return Double.NaN;
            }
            if ((alpha >= 1.0D) || (alpha <= 0.0D))
            {
               // throw new OutOfRangeException(LocalizedFormats.SIGNIFICANCE_LEVEL, Double.valueOf(alpha), Integer.valueOf(0), Integer.valueOf(1));
            }
            TDistribution distribution = new TDistribution(this.n - 2L);
            return GetSlopeStdErr() * distribution.inverseCumulativeProbability(1.0D - alpha / 2.0D);
        }

        public double GetSignificance()
        {
            if (this.n < 3L)
            {
                return Double.NaN;
            }
            TDistribution distribution = new TDistribution(this.n - 2L);
            return 2.0D * (1.0D - distribution.cumulativeProbability(Math.Abs(GetSlope()) / GetSlopeStdErr()));
        }

        private double GetIntercept(double slope)
        {
            if (this.hasIntercept)
            {
                return (this.sumY - slope * this.sumX) / this.n;
            }
            return 0.0D;
        }

        private double GetRegressionSumSquares(double slope)
        {
            return slope * slope * this.sumXX;
        }
        /**
            public RegressionResults Regress()
                {
                    throw new NotImplementedException();
                }

                public RegressionResults Regress(int[] paramArrayOfInt)
                {
                    throw new NotImplementedException();
                }**/
    }
}
