using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class ArtaExecutionContext
    {
        public IArtaProcess Arta { get; set; }
        public IBaseDistribution State { get; set; }
        public double[] ArtaCorrelationCoefficients { get; set; }

        public ArtaExecutionContext(IBaseDistribution state, double[] artaCorrelationCoefficients)
        {
            State = state;
            ArtaCorrelationCoefficients = artaCorrelationCoefficients;
        }

        public IArtaProcess CreateArtaProcess()
        {
            if (ArtaCorrelationCoefficients == null)
            {
                ArtaCorrelationCoefficients = new double[] { 0.0 };
            }
            AutoCorrelation.GetCorrelationMatrix(ArtaCorrelationCoefficients).Cholesky();
            return State.CreateArtaProcess(ArtaCorrelationCoefficients, new MersenneTwister());
        }




    }
}
