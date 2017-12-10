using MathNet.Numerics.Random;

namespace Arta.Distribution
{
    public abstract class BaseDistribution
    {
        public abstract AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random);
        public abstract double InverseCumulativeDistribution(double p);
        public abstract double GetMean();
        public abstract double GetVariance();

        public enum Distribution
        {
            NormalDistribution,
            ContinousUniformDistribution,
            ExponentialDistribution
        }
    }
}
