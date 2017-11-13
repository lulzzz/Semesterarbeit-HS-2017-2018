using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Math
{
    class AbstractRealDistribution : IRealDistribution
    {
        public const double SolverDefaultAbsoluteAccuracy = 1.0 - 6D;
        protected readonly RandomSource random;

        public AbstractRealDistribution(RandomSource rng)
        {
            this.random = rng;
        }

        public double Mode { get; }

        public double Minimum { get; }

        public double Maximum { get; }

        public double Mean { get; }

        public double Variance { get; }

        public double StdDev { get; }

        public double Entropy { get; }

        public double Skewness { get; }

        public double Median { get; }

        public Random RandomSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double CumulativeDistribution(double x)
        {
            throw new NotImplementedException();
        }

        public double CumulativeProbability(double paramDouble)
        {
            throw new NotImplementedException();
        }

        public double CumulativeProbability(double paramDouble1, double paramDouble2)
        {
            return Probability(paramDouble1, paramDouble2);
        }

        public double Probability(double paramDouble1, double paramDouble2)
        {
            if(paramDouble1 > paramDouble2)
            {
                throw new OverflowException("Number is too large");
            }
            return CumulativeProbability(paramDouble1) - CumulativeProbability(paramDouble2);
        }

        public double Density(double x)
        {
            throw new NotImplementedException();
        }

        public double DensityLn(double x)
        {
            throw new NotImplementedException();
        }

        public double GetSupportLowerBound()
        {
            throw new NotImplementedException();
        }

        public double GetSupportUpperBound()
        {
            throw new NotImplementedException();
        }

        public double InverseCumulativeProbability(double paramDouble)
        {
            throw new NotImplementedException();
        }

        public bool IsSupportConnected()
        {
            throw new NotImplementedException();
        }

        public bool IsSupportLowerBoundInclusive()
        {
            throw new NotImplementedException();
        }

        public bool IsSupportUpperBoundInclusive()
        {
            throw new NotImplementedException();
        }

        public double Probability(double paramDouble)
        {
            throw new NotImplementedException();
        }

        public double Sample()
        {
            throw new NotImplementedException();
        }

        public void Samples(double[] values)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> Samples()
        {
            throw new NotImplementedException();
        }
    }
}
