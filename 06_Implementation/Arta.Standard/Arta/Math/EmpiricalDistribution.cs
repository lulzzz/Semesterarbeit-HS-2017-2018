using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using MathNet.Numerics.Statistics;
using System;

namespace Arta.Math
{
    public class EmpiricalDistribution : ContinuousUniform
    {
        private int nPoints;
        private double[] points;
        private double mean, variance, lowerBound, upperBound;
        Random random;

        public EmpiricalDistribution(double[] values)
        {
            random = SystemRandomSource.Default;
            nPoints = values.Length;
            points = new double[values.Length];
            Array.Copy(values, points, nPoints);
            Array.Sort(points);
            lowerBound = points[0];
            upperBound = points[nPoints - 1];
            mean = Statistics.Mean(values);
            variance = Statistics.Variance(values);
        }

        public double InverseCumulativeProbability(double p)
        {
            if (p <= 0.0 || p >= 1.0)
            {
                if (p == 1.0)
                {
                    return upperBound;
                }
                if (p == 0.0)
                {
                    return lowerBound;
                }
                throw new IndexOutOfRangeException("p was out of Range");
            }
            var index = (int)(p * nPoints);
            return points[index];
        }
    }
}
