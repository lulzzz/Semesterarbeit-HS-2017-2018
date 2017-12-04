using Arta.Math;
using Arta.Tests;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;

namespace Arta
{
    public class ArProcessFactory
    {
        private ArProcessFactory() { }

        public static ArProcess CreateArProcess(double[] arAutocorrelations)
        {
            return CreateArProcess(arAutocorrelations, new MersenneTwister());
        }

        public static ArProcess CreateArProcess(double[] arAutocorrelations, RandomSource random)
        {
            var alphas = ArAutocorrelationsToAlphas(arAutocorrelations);
            if (!StationaryTest.IsStationary(alphas))
            {
                throw new NotStationaryException("Underlying ar-process is not stationary");
            }

            var variance = CalculateVariance(arAutocorrelations, alphas);
            var whiteNoiseProcess = new Normal(random);
            return new ArProcess(alphas, whiteNoiseProcess);
        }

        public static double[] ArAutocorrelationsToAlphas(double[] arAutocorrelations)
        {
            var dim = arAutocorrelations.Length;
            var alphas = new double[dim];
            var psi = AutoCorrelation.GetCorrelationMatrix(arAutocorrelations);
            var r = CreateMatrix.DenseOfColumnArrays(arAutocorrelations).Transpose();
            var a = r.Multiply(psi.Cholesky().Solve(psi).Inverse());
            alphas = a.Row(0).AsArray();
            return alphas;
        }

        public static double CalculateVariance(double[] arAutoCorrelations, double[] alphas)
        {
            var variance = 1.0;
            for(var i = 0; i < alphas.Length; i++)
            {
                variance = variance - alphas[i] * arAutoCorrelations[i];
            }
            return variance;
        }
    }
}
