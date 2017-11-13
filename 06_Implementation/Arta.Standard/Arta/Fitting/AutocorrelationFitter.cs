using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Fitting
{
    class AutocorrelationFitter
    {
        private readonly ArtaCorrelationEstimator artaCorrelationEstimator;

        public AutocorrelationFitter(ContinuousUniform distribution)
        {
            this.artaCorrelationEstimator = new ArtaCorrelationEstimator(distribution);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutoCorrealtions, double maxError)
        {
            int dim = desiredArtaAutoCorrealtions.Length;
            double[] maxErrors = new double[dim];
            for(int i = 0; i < dim; i++)
            {
                maxErrors[i] = maxError;
            }
            return FitArAutocorrelations(desiredArtaAutoCorrealtions, maxErrors);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutocorrelations, double[] maxErrors)
        {
            int dim = desiredArtaAutocorrelations.Length;
            double[] result = new double[dim];
            for(int i = 0; i < dim; i++)
            {
                result[i] = FitArAutocorrelations(desiredArtaAutocorrelations[i], maxErrors[i]);
            }
            return result;
        }

        public double FitArAutocorrelations(double desiredArtaAutocorrelation, double maxError)
        {
            double lowerBound = -1.0;
            double upperBound = 1.0;
            double centerPoint;
            double estimatedCorrelation;

            estimatedCorrelation = artaCorrelationEstimator.EstimateArtaCorrelation(upperBound);
            if(System.Math.Abs(desiredArtaAutocorrelation - estimatedCorrelation) < maxError)
            {
                return lowerBound;
            }

            estimatedCorrelation = artaCorrelationEstimator.EstimateArtaCorrelation(upperBound);
            if(System.Math.Abs(desiredArtaAutocorrelation - estimatedCorrelation) < maxError)
            {
                return upperBound;
            }

            do
            {
                centerPoint = (lowerBound + upperBound) / 2.0;
                estimatedCorrelation = artaCorrelationEstimator.EstimateArtaCorrelation(centerPoint);
                if (desiredArtaAutocorrelation < estimatedCorrelation)
                {
                    upperBound = centerPoint;
                }
                else
                {
                    lowerBound = centerPoint;
                }
            } while (System.Math.Abs(desiredArtaAutocorrelation - estimatedCorrelation) > maxError);

            return centerPoint;
        }
    }
}
