using Math3.random;
using Math3.util;
using System;

namespace MathSubSet.ArtaExtensions
{
    public abstract class AbstractRealDistribution : IRealDistribution
    {
        public static readonly double SOLVER_DEFAULT_ABSOLUTE_ACCURACY = 1.0E-6D;
        [Obsolete]
        protected RandomDataImpl randomData = new RandomDataImpl();
        protected readonly RandomGenerator random;
        private double solverAbsoluteAccuracy = 1.0E-6D;

        [Obsolete]
        protected AbstractRealDistribution()
        {
            this.random = null;
        }

        protected AbstractRealDistribution(RandomGenerator rng)
        {
            this.random = rng;
        }

        [Obsolete]
        public double CumulativeProbability(double x0, double x1) //throws NumberIsTooLargeException
        {
            return Probability(x0, x1);
        }

        public double Probability(double x0, double x1)
        {
            if (x0 > x1)
            {
                //throw new NumberIsTooLargeException(LocalizedFormats.LOWER_ENDPOINT_ABOVE_UPPER_ENDPOINT, Double.valueOf(x0), Double.valueOf(x1), true);
            }
            return CumulativeProbability(x1) - CumulativeProbability(x0);
        }
        /*
        public double InverseCumulativeProbability(double p)
        {
            if ((p < 0.0D) || (p > 1.0D))
            {
                // throw new OutOfRangeException(Double.valueOf(p), Integer.valueOf(0), Integer.valueOf(1));
            }
            double lowerBound = GetSupportLowerBound();
            if (p == 0.0D)
            {
                return lowerBound;
            }
            double upperBound = GetSupportUpperBound();
            if (p == 1.0D)
            {
                return upperBound;
            }

            double mu = GetNumericalMean();
            double sig = FastMath.sqrt(GetNumericalVariance());

            bool chebyshevApplies = (!Double.IsInfinity(mu)) && (!Double.IsNaN(mu)) && (!Double.IsInfinity(sig)) && (!Double.IsNaN(sig));
            if (lowerBound == Double.NegativeInfinity)
            {
                if (chebyshevApplies)
                {
                    lowerBound = mu - sig * FastMath.sqrt((1.0D - p) / p);
                }
                else
                {
                    lowerBound = -1.0D;
                    while (InverseCumulativeProbability(lowerBound) >= p)
                    {
                        lowerBound *= 2.0D;
                    }
                }
            }
            if (upperBound == Double.PositiveInfinity)
            {
                if (chebyshevApplies)
                {
                    upperBound = mu + sig * FastMath.sqrt(p / (1.0D - p));
                }
                else
                {
                    upperBound = 1.0D;
                    while (CumulativeProbability(upperBound) < p)
                    {
                        upperBound *= 2.0D;
                    }
                }
            }
            
        }
        */
        protected double GetSolverAbsoluteAccuracy()
        {
            return this.solverAbsoluteAccuracy;
        }

        public void ReseedRandomGenerator(long seed)
        {
            random.setSeed(seed);
            randomData.reSeed(seed);
        }
        /*
        public double Sample()
        {
            return InverseCumulativeProbability(this.random.nextDouble());
        }
        */
        public double[] Sample(int sampleSize)
        {
            if (sampleSize <= 0)
            {
                // throw new NotStrictlyPositiveException(LocalizedFormats.NUMBER_OF_SAMPLES, Integer.valueOf(sampleSize));
            }
            double[] outVar = new double[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                outVar[i] = Sample();
            }
            return outVar;
        }

        public double Probability(double x)
        {
            return 0.0D;
        }

        public double LogDensity(double x)
        {
            return FastMath.log(Density(x));
        }

        public abstract double Density(double paramDouble);
        public abstract double CumulativeProbability(double paramDouble);
        public abstract double GetNumericalMean();
        public abstract double GetNumericalVariance();
        public abstract double GetSupportLowerBound();
        public abstract double GetSupportUpperBound();
        public abstract bool IsSupportLowerBoundInclusive();
        public abstract bool IsSupportUpperBoundInclusive();
        public abstract bool IsSupportConnected();

        public double InverseCumulativeProbability(double paramDouble)
        {
            throw new NotImplementedException();
        }

        public double Sample()
        {
            throw new NotImplementedException();
        }
    }
}
