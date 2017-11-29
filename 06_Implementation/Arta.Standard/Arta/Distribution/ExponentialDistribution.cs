using MathNet.Numerics.Distributions;
using System;
using MathNet.Numerics.Random;
using Arta.Fitting;

namespace Arta.Math
{
    public class ExponentialDistribution : IBaseDistribution
    {
        private readonly Exponential exponential;
        private const double DefaultError = 0.0001;

        public ExponentialDistribution()
        {
            exponential = new Exponential(1);
        }

        public double GetMean()
        {
            return exponential.Mean;
        }

        public double GetVariance()
        {
            return exponential.Variance;
        }

        public double InverseCumulativeDistribution(double p)
        {
            return exponential.InverseCumulativeDistribution(p);
        }

        public AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random)
        {
            var fitter = new AutocorrelationFitter(this);
            var arCorrelationCOefficients = fitter.FitArAutocorrelations(artaCorrelationCoefficients, DefaultError);
            var ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);

            return new ArtaProcessGeneral(ar, this);
        }
    }
}
