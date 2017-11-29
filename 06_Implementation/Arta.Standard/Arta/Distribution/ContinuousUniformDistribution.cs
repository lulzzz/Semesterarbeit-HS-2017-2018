using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Arta.Math
{
    public class ContinuousUniformDistribution : IDistribution
    {
        ContinuousUniform continuousUniform;

        public void Handle(ArtaExecutionContext context)
        {
            continuousUniform = new ContinuousUniform(-1, 1);
        }

        public double InverseCumulativeDistribution(double p)
        {
            return continuousUniform.InverseCumulativeDistribution(p);
        }
        public double GetLowerBound()
        {
            return continuousUniform.LowerBound;
        }

        public double GetMean()
        {
            return continuousUniform.Mean;
        }

        public double GetUpperBound()
        {
            return continuousUniform.UpperBound;
        }

        public double GetVariance()
        {
            return continuousUniform.Variance;
        }

        public AbstractArtaProcess CreateArtaProcess(double[] artaCorrelationCoefficients, RandomSource random)
        {
            var dim = artaCorrelationCoefficients.Length;
            double[] arCorrelationCoefficients = new double[dim];
            for (var i = 0; i < dim; i++)
            {
                arCorrelationCoefficients[i] = 2 * System.Math.Sin(System.Math.PI * artaCorrelationCoefficients[i] / 6);
            }
            ArProcess ar = ArProcessFactory.CreateArProcess(arCorrelationCoefficients, random);
            return new ArtaProcessUniform(ar, continuousUniform.LowerBound, continuousUniform.UpperBound);
        }
    }
}
