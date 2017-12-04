using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class ArtaExecutionContext
    {
        public IArtaProcess Arta { get; set; }
        public BaseDistribution distribution { get; set; }
        public double[] ArtaCorrelationCoefficients { get; set; }

        public ArtaExecutionContext(BaseDistribution.Distribution distribution, double[] artaCorrelationCoefficients)
        {
            this.distribution = DistributionFactory.CreateDistribution(distribution);
            ArtaCorrelationCoefficients = artaCorrelationCoefficients;
        }

        public IArtaProcess CreateArtaProcess()
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
