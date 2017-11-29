using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class ArtaExecutionContext
    {
        public IArtaProcess Arta { get; set; }
        public IDistribution State { get; set; }
        public double[] ArtaCorrelationCoefficients { get; set; }

        public ArtaExecutionContext(IDistribution state, double[] artaCorrelationCoefficients)
        {
            State = state;
            ArtaCorrelationCoefficients = artaCorrelationCoefficients;
            State.Handle(this);
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
