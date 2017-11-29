using MathNet.Numerics.Random;

namespace Arta.Math
{
    public interface IDistribution
    {
        void Handle(ArtaExecutionContext context);
        AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random);

        double InverseCumulativeDistribution(double p);
        double GetMean();
        double GetVariance();
    }
}
