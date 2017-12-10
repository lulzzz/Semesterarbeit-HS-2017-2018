using Arta.Distribution;
using Arta.Math;
using MathNet.Numerics.Random;

namespace Arta
{
    public class ArtaExecutionContext
    {
        public IArtaProcess ArtaProcess { get; private set; }
        public BaseDistribution distribution { get; private set; }
        public double[] ArtaCorrelationCoefficients { get; set; }


        public ArtaExecutionContext(BaseDistribution.Distribution distribution, double[] artaCorrelationCoefficients)
        {
            this.distribution = DistributionFactory.CreateDistribution(distribution);
            ArtaCorrelationCoefficients = artaCorrelationCoefficients;
            ArtaProcess = CreateArtaProcess();
        }

        private IArtaProcess CreateArtaProcess()
        {
            if (ArtaCorrelationCoefficients == null)
            {
                ArtaCorrelationCoefficients = new double[] { 0.0 };
            }
            AutoCorrelation.GetCorrelationMatrix(ArtaCorrelationCoefficients).Cholesky();
            return distribution.CreateArtaProcess(ArtaCorrelationCoefficients, new MersenneTwister());
        }
    }
}
