using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MersenneTwister;
using Math3.random;
using ARTA_Libary.ch.hsr.artac.tests;
using Math3.distribution;
using Math3.util;
using Math3.linear;
using ARTA_Libary.ch.hsr.artac.math;

namespace ARTA_Libary.ch.hsr.artac.arta
{
    internal class ArProcessFactory
    {
        private ArProcessFactory()
        {
            // no instantiation
        }

        public static ArProcess CreateArProcess(double[] arAutocorrelations)
        {
            try
            {
                return CreateArProcess(arAutocorrelations, new MersenneTwister());
            }
            catch(NotStationaryException e)
            {
                Console.WriteLine(e.StackTrace);
                
            }

        }

        public static ArProcess CreateArProcess(double[] arAutocorrelations, RandomGenerator random)
        {
            try
            {
                double[] alphas = ArAutocorrelationsToAlphas(arAutocorrelations);
                if (!StationaryTest.IsStationary(alphas))
                {
                    throw new NotStationaryException();
                }
                double variance = CalculateVariance(arAutocorrelations, alphas);
                //TODO check if variance is positive? otherwise exception
                NormalDistribution whiteNoiseProcess = new NormalDistribution(random, 0.0, FastMath.sqrt(variance), NormalDistribution.DEFAULT_INVERSE_ABSOLUTE_ACCURACY);
                return new ArProcess(alphas, whiteNoiseProcess);
            }
            catch(NotStationaryException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static double[] ArAutocorrelationsToAlphas(double[] arAutocorrelations)
        {
            int dim = arAutocorrelations.Length;
            double[] alphas = new double[dim];

            RealMatrix psi = AutoCorrelation.GetCorrelationMatrix(arAutocorrelations);
            RealMatrix r = new Array2DRowRealMatrix(arAutocorrelations).transpose();
            RealMatrix a = r.multiply(new CholeskyDecomposition(psi).getSolver().getInverse());
            alphas = a.getRow(0);
            return alphas;
        }

        public static double CalculateVariance(double[] arAutocorrelations, double[] alphas)
        {
            double variance = 1.0;
            for(int i = 0; i < alphas.Length; i++)
            {
                variance -= alphas[i] * arAutocorrelations[i];
            }
            return variance;
        }
    }
}
