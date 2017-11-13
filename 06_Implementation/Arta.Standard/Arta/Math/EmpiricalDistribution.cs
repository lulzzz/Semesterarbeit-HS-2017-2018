using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Math
{
    class EmpiricalDistribution : ContinuousUniform
    {
        private readonly int nPoints;
        private readonly double[] points;
        private readonly double mean, variance, lowerBound, upperBound;

        public EmpiricalDistribution(double[] values) : base()
        {
            this.nPoints = values.Length;
            Array.Copy(values, points, nPoints);
            Array.Sort(points);
            this.lowerBound = points[0];
            this.upperBound = points[nPoints - 1];
            this.mean = Statistics.Mean(values);
            this.variance = Statistics.Variance(values);
        }

        public double InverseCumulativeProbability(double p)
        {
            if(p <= 0.0 || p >= 1.0)
            {
                if(p == 1.0)
                {
                    return upperBound;
                }
                if(p == 0.0)
                {
                    throw new IndexOutOfRangeException("p was 0.0");
                }
            }
            int index = (int)(p * nPoints);
            return points[index];
        }
    }
}
