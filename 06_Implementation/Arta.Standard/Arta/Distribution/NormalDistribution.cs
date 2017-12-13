using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Arta.Distribution
{
    public class NormalDistribution : IBaseDistribution
    {
        private readonly Normal normal;

        public NormalDistribution()
        {
            normal = new Normal();
        }

        public double InverseCumulativeDistribution(double p)
        {
            return normal.InverseCumulativeDistribution(p);
        }

        public double GetMean()
        {
            return normal.Mean;
        }

        public double GetVariance()
        {
            return normal.Variance;
        }

        public AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random)
        {
            ArProcess arProcess = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);
            return new ArtaProcessNormal(arProcess, normal.Mean, normal.Variance);
        }
        public override string ToString()
        {
            return " Normal";
        }
    }
}
