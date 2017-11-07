using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.fitting
{
    class AutocorrelationFitter
    {
        internal ArtaCorrelationEstimator artaCorrelationEstimator;

        public AutocorrelationFitter(RealDistribution dist)
        {
            this.artaCorrelationEstimator = new ArtaCorrelationEstimator(dist);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutocorrelations, double maxError)
        {
            int dim = desiredArtaAutocorrelations.Length;
            double[] maxErrors = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                maxErrors[i] = maxError;
            }
            return FitArAutocorrelations(desiredArtaAutocorrelations, maxErrors);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutocorrelations, double[] maxErrors)
        {
            int dim = desiredArtaAutocorrelations.Length;
            double[] result = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                result[i] = FitArAutocorrelation(desiredArtaAutocorrelations[i], maxErrors[i]);
            }
            return result;
        }

        public double FitArAutocorrelation(double desiredArtaCorrealtion, double maxError)
        {
            double lowerBound = -1.0;
            double upperBound = 1.0;
            double centerPoint;
            double estimatedCorrelation;

            estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(lowerBound);
            if (Math.Abs(desiredArtaCorrealtion - estimatedCorrelation) < maxError)
            {
                return lowerBound;
            }

            estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(upperBound);
            if (Math.Abs(desiredArtaCorrealtion - estimatedCorrelation) > maxError)
            {
                return upperBound;
            }

            /**
             * 1. Calculate centr point and estimate correlation at center point
             * 2. update bounds to reflect new search interval
             * 3. if estimated correlation is not a good fit, return to step 1
             * */
            do
            {
                centerPoint = (lowerBound + upperBound) / 2.0;
                estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(centerPoint);
                if (desiredArtaCorrealtion < estimatedCorrelation)
                {
                    upperBound = centerPoint;
                }
                else
                {
                    lowerBound = centerPoint;
                }
            }
            while (Math.Abs(desiredArtaCorrealtion - estimatedCorrelation) > maxError);
            
            return centerPoint;
            
    
        }
    }
}
