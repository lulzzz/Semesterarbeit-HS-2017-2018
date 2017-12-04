using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class NormalDistribution : BaseDistribution
    {
        private readonly Normal normal;

        public NormalDistribution()
        {
            normal = new Normal(0, 1);
        }

        public override double InverseCumulativeDistribution(double p)
        {
            return normal.InverseCumulativeDistribution(p);
        }

        public override double GetMean()
        {
            return normal.Mean;
        }

        public override double GetVariance()
        {
            return normal.Variance;
        }

        public override AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random)
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
