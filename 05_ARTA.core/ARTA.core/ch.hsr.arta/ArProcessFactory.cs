using ARTA.core.ch.hsr.math;
using Math3.distribution;
using Math3.linear;
using Math3.random;
using System;

namespace ARTA.core.ch.hsr.arta
{
    class ArProcessFactory
    {
        private ArProcessFactory()
        {
            //no instantiation
        }

        public ArProcess CreateArProcess(double[] arAutocorrealtions)
        {
            return CreateArProcess(arAutocorrealtions, new Well19937c());
        }

        public ArProcess CreateArProcess(double[] arAutocorrelations, RandomGenerator random)
        {//TODO Exception-Handling
                double[] alphas = ArAutocorrelationsToAlphas(arAutocorrelations);
                double variance = CalculateVariance(arAutocorrelations, alphas);
                NormalDistribution whiteNoiseProcess = new NormalDistribution(random, 0.0, Math.Sqrt(variance), NormalDistribution.DEFAULT_INVERSE_ABSOLUTE_ACCURACY);
          
            return new ArProcess(alphas, whiteNoiseProcess);
        }

        /**
         * Determines the coefficients (alpha) for the ARTA-Process.
         * */
        public double[] ArAutocorrelationsToAlphas(double[] arAutocorrelations)
        {
            int dim = arAutocorrelations.Length;
            double[] alphas = new double[dim];

            RealMatrix psi = AutoCorrelation.GetCorrelationMatrix(arAutocorrelations);
            RealMatrix r = new Array2DRowRealMatrix(arAutocorrelations).transpose();
            RealMatrix a = r.multiply(new CholeskyDecomposition(psi).getSolver().getInverse());
            alphas = a.getRow(0);
            return alphas;
        }

        public double CalculateVariance(double[] arAutocorrelations, double[] alphas)
        {
            double variance = 0;
            for (int i = 0; i < alphas.Length; i++)
            {
                variance = variance - alphas[i] * arAutocorrelations[i];
            }
            return variance;
        }
    }
}
