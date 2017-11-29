using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class NormalDistribution : IBaseDistribution
    {
        private Normal normal;

        public void Handle(ArtaExecutionContext context)
        {
            normal = new Normal(0, 1);
        }

        public double InverseCumulativeDistribution(double p)
        {
            return normal.InverseCumulativeDistribution(p);
        }

        public double GetLowerBound()
        {
            throw new System.NotImplementedException();
        }

        public double GetMean()
        {
            return normal.Mean;
        }

        public double GetUpperBound()
        {
            throw new System.NotImplementedException();
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
    }
}
