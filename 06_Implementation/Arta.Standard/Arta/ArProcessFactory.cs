using Arta.Math;
using Arta.Tests;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    class ArProcessFactory
    {
        private static Cholesky<Double> cholesky;
        private ArProcessFactory() { }

        public static ArProcess CreateArProcess(double[] arAutocorrelations)
        {
            return CreateArProcess(arAutocorrelations, new MersenneTwister());
        }

        public static ArProcess CreateArProcess(double[] arAutocorrelations, RandomSource random)
        {
            double[] alphas = ArAutocorrelationsToAlphas(arAutocorrelations);
            if (!StationaryTest.IsStationary(alphas))
            {
                throw new NotStationaryException("Underlying ar-process is not stationary");
            }

            double variance = CalculateVariance(arAutocorrelations, alphas);
            Normal whiteNoiseProcess = new Normal(random);
            return new ArProcess(alphas, whiteNoiseProcess);
        }

        public static double[] ArAutocorrelationsToAlphas(double[] arAutocorrelations)
        {
            int dim = arAutocorrelations.Length;
            double[] alphas = new double[dim];
            Matrix<double> psi = AutoCorrelation.GetCorrelationMatrix(arAutocorrelations);
            Matrix<double> r = CreateMatrix.DenseOfColumnArrays(arAutocorrelations).Transpose();
            Matrix<double> a = r.Multiply(cholesky.Solve(psi).Inverse());
            alphas = a.Row(0).AsArray();
            return alphas;
        }

        public static double CalculateVariance(double[] arAutoCorrelations, double[] alphas)
        {
            double variance = 1.0;
            for(int i = 0; i < alphas.Length; i++)
            {
                variance = variance - alphas[i] * arAutoCorrelations[i];
            }
            return variance;
        }
    }
}
