using MathNet.Numerics.Distributions;
using System;
using MathNet.Numerics.Random;
using Arta.Fitting;

namespace Arta.Math
{
    public class ExponentialDistribution : IDistribution
    {
        Exponential exponential;
        private const double DefaultError = 0.0001;


        public void Handle(ArtaExecutionContext context)
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
            ArtaProcessGeneral arta = null;
            AutocorrelationFitter fitter = new AutocorrelationFitter(this);
            double[] arCorrelationCOefficients = fitter.FitArAutocorrelations(artaCorrelationCoefficients, DefaultError);
            ArProcess ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);
            arta = new ArtaProcessGeneral(ar, this);
            return arta;
        }
    }
}
