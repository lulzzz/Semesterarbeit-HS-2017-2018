using Math3.distribution;
using Math3.random;
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

        public UniformRealDistribution() : this(0.0D, 1.0D)
        {
        }

        public UniformRealDistribution(double lower, double upper) : this(new Well19937c(), lower, upper) //throws NumberIsTooLargeException
        {
        }

        public UniformRealDistribution(double lower, double upper, double inverseCumAccuracy) : this(new Well19937c(), lower, upper) //throws NumberIsTooLargeException
        {
            
        }

        public UniformRealDistribution(RandomGenerator rng, double lower, double upper, double inverseCumAccuracy) : this(rng, lower, upper)
        {
            
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

        public override double density(double x)
        {
            if ((x < this.lower) || (x > this.upper))
            {
                return 0.0D;
            }
            return 1.0D / (this.upper - this.lower);
        }

        public override double cumulativeProbability(double x)
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

        public new double inverseCumulativeProbability(double p) //throws OutOfRangeException
        {
            if ((p < 0.0D) || (p > 1.0D))
            {
                // throw new OutOfRangeException(Double.valueOf(p), Integer.valueOf(0), Integer.valueOf(1));
            }
            return p * (this.upper - this.lower) + this.lower;
        }

        public override double getNumericalMean()
        {
            return 0.5D * (this.lower + this.upper);
        }

        public override double getNumericalVariance()
        {
            double ul = this.upper - this.lower;
            return ul * ul / 12.0D;
        }

        public override double getSupportLowerBound()
        {
            return this.lower;
        }

        public override double getSupportUpperBound()
        {
            return this.upper;
        }

        public override bool isSupportLowerBoundInclusive()
        {
            return true;
        }

        public override bool isSupportUpperBoundInclusive()
        {
            return true;
        }

        public override bool isSupportConnected()
        {
            return true;
        }

        public double sample()
        {
            double u = this.random.nextDouble();
            return u * this.upper + (1.0D - u) * this.lower;
        }
    }
}
