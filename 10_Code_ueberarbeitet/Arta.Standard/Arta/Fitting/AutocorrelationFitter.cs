using Arta.Distribution;

namespace Arta.Fitting
{
    public class AutocorrelationFitter
    {
        private ArtaCorrelationEstimator artaCorrelationEstimator;

        public AutocorrelationFitter(IBaseDistribution distribution)
        {
            artaCorrelationEstimator = new ArtaCorrelationEstimator(distribution);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutoCorrealtions, double maxError)
        {
            var dim = desiredArtaAutoCorrealtions.Length;
            var maxErrors = new double[dim];
            for(var i = 0; i < dim; i++)
            {
                maxErrors[i] = maxError;
            }
            return FitArAutocorrelations(desiredArtaAutoCorrealtions, maxErrors);
        }

        public double[] FitArAutocorrelations(double[] desiredArtaAutocorrelations, double[] maxErrors)
        {
            var dim = desiredArtaAutocorrelations.Length;
            var result = new double[dim];
            for(var i = 0; i < dim; i++)
            {
                result[i] = FitArAutocorrelations(desiredArtaAutocorrelations[i], maxErrors[i]);
            }
            return result;
        }

        public double FitArAutocorrelations(double desiredArtaAutocorrelation, double maxError)
        {
            var lowerBound = -1.0;
            var upperBound = 1.0;
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
