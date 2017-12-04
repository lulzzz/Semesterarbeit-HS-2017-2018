using Arta.Fitting;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class ExponentialDistribution : BaseDistribution
    {
        private readonly Exponential exponential;
        private const double DefaultError = 0.0001;

        public ExponentialDistribution()
        {
            exponential = new Exponential(1);
        }

        public override double GetMean()
        {
            return exponential.Mean;
        }

        public override double GetVariance()
        {
            return exponential.Variance;
        }

        public override double InverseCumulativeDistribution(double p)
        {
            return exponential.InverseCumulativeDistribution(p);
        }

        public override AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random)
        {
            var fitter = new AutocorrelationFitter(this);
            var arCorrelationCOefficients = fitter.FitArAutocorrelations(artaCorrelationCoefficients, DefaultError);
            var ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);

            return new ArtaProcessGeneral(ar, this);
        }
        public override string ToString()
        {
            return " Exponential";
        }
    }
}
