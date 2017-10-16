using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet
{
    class UniformRealDistribution : AbstractRealDistribution
    {
        public static readonly double DEFAULT_INVERSE_ABSOLUTE_ACCURACY = 1.0E-9D;
        private static readonly long serialVersionUID = 20120109L;
        private readonly double lower;
        private readonly double upper;

        public UniformRealDistribution()
        {
            this(0.0D, 1.0D);
        }

        public UniformRealDistribution(double lower, double upper) //throws NumberIsTooLargeException
        {
            this(new Well19937c(), lower, upper);
        }

        public UniformRealDistribution(double lower, double upper, double inverseCumAccuracy) //throws NumberIsTooLargeException
        {
            this(new Well19937c(), lower, upper);
        }

        public UniformRealDistribution(RandomGenerator rng, double lower, double upper, double inverseCumAccuracy)
        {
            this(rng, lower, upper);
        }

        public UniformRealDistribution(RandomGenerator rng, double lower, double upper) : base(rng) //throws NumberIsTooLargeException
        {
            if (lower >= upper)
            {
                // throw new NumberIsTooLargeException(LocalizedFormats.LOWER_BOUND_NOT_BELOW_UPPER_BOUND, Double.valueOf(lower), Double.valueOf(upper), false);
            }
            this.lower = lower;
            this.upper = upper;
        }

        public double Density(double x)
        {
            if ((x < this.lower) || (x > this.upper))
            {
                return 0.0D;
            }
            return 1.0D / (this.upper - this.lower);
        }

        public double CumulativeProbability(double x)
        {
            if (x <= this.lower)
            {
                return 0.0D;
            }
            if (x >= this.upper)
            {
                return 1.0D;
            }
            return (x - this.lower) / (this.upper - this.lower);
        }

        public double InverseCumulativeProbability(double p) //throws OutOfRangeException
        {
            if ((p < 0.0D) || (p > 1.0D))
            {
                // throw new OutOfRangeException(Double.valueOf(p), Integer.valueOf(0), Integer.valueOf(1));
            }
            return p * (this.upper - this.lower) + this.lower;
        }

        public double GetNumericalMean()
        {
            return 0.5D * (this.lower + this.upper);
        }

        public double GetNumericalVariance()
        {
            double ul = this.upper - this.lower;
            return ul * ul / 12.0D;
        }

        public double GetSupportLowerBound()
        {
            return this.lower;
        }

        public double GetSupportUpperBound()
        {
            return this.upper;
        }

        public bool IsSupportLowerBoundInclusive()
        {
            return true;
        }

        public bool IsSupportUpperBoundInclusive()
        {
            return true;
        }

        public bool IsSupportConnected()
        {
            return true;
        }

        public double Sample()
        {
            double u = this.random.nextDouble();
            return u * this.upper + (1.0D - u) * this.lower;
        }
    }
}
