using System;
using MathSubSet;
using MathSubSet.ArtaExtensions;

namespace ARTA.core.ch.hsr.math
{
    class EmpiricalDistribution : Math3.distribution.AbstractRealDistribution
    {
        private readonly int nPoints;
        private readonly double[] points;
        private readonly double mean;
        private readonly double variance;
        private readonly double lowerBound;
        private readonly double upperBound;

        public EmpiricalDistribution(double[] values) : base(new Well1024a())
        {
            this.nPoints = values.Length;
            Array.Copy(values, points, nPoints);
            Array.Sort(points);
            this.lowerBound = points[0];
            this.upperBound = points[nPoints - 1];
            this.mean = StatHelper.Mean(values);
            // populationVariance vs. Variance
            this.variance = StatHelper.Variance(values);

        }
        public override double cumulativeProbability(double x)
        {
            return 0;
        }

        public override double density(double x)
        {
            return 0;
        }

        public override double getNumericalMean()
        {
            return mean;
        }

        public override double getNumericalVariance()
        {
            return variance;
        }

        public override double getSupportLowerBound()
        {
            return lowerBound;
        }

        public override double getSupportUpperBound()
        {
            return upperBound;
        }

        public override bool isSupportConnected()
        {
            return true;
        }

        public override bool isSupportLowerBoundInclusive()
        {
            return true;
        }

        public override bool isSupportUpperBoundInclusive()
        {
            return true;
        }
    }
}
