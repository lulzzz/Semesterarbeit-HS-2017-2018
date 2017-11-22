using Arta.Fitting;
using Arta.Math;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    public class ArtaProcessFactory
    {
        public const double DefaultError = 0.0001;
        public static Cholesky<double> cholesky;

        private ArtaProcessFactory() { }

        public static IArtaProcess CreateArtaProcess(double[] data)
        {
            EmpiricalDistribution distribution = new EmpiricalDistribution(data);
            var order = OrderEstimator.EstimateOrder(data);
            double[] artaCorrelationCoefficients = new double[order + 1];
            Array.Copy(AutoCorrelation.CalculateAcfs(data, order), artaCorrelationCoefficients, order + 1);
            return CreateArtaProcess(distribution, artaCorrelationCoefficients, new MersenneTwister());
        }

        public static IArtaProcess CreateArtaProcess(ContinuousUniform distribution, double[] artaCorrelationCoefficients)
        {
            return CreateArtaProcess(distribution, artaCorrelationCoefficients, new MersenneTwister());
        }

        public static IArtaProcess CreateArtaProcess(ContinuousUniform distribution, double[] artaCorrelationCoefficients, RandomSource random)
        {
            AbstractArtaProcess arta = null;
            if(artaCorrelationCoefficients == null)
            {
                double[] noCorrelation = { 0.0 };
                artaCorrelationCoefficients = noCorrelation;
            }
            //TODO Cholesky Check
            //TODO Feasabilitycheck

            /*
            if(distribution is Normal)
            {
                arta = CreateArtaProcessN((Normal)distribution, artaCorrelationCoefficients, random);
            } else */if(distribution is ContinuousUniform){
                arta = CreateArtaProcessU((ContinuousUniform)distribution, artaCorrelationCoefficients, random);
            }
            else{
                arta = CreateArtaProcessG(distribution, artaCorrelationCoefficients, random);
            }
            return arta;
        }

        private static ArtaProcessGeneral CreateArtaProcessG(ContinuousUniform distribution, double[] artaCorrelationCoefficients, RandomSource random)
        {
            ArtaProcessGeneral arta = null;
            AutocorrelationFitter fitter = new AutocorrelationFitter(distribution);
            double[] arCorrelationCOefficients = fitter.FitArAutocorrelations(artaCorrelationCoefficients, DefaultError);
            ArProcess ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);
            arta = new ArtaProcessGeneral(ar, distribution);
            return arta;
        }

        private static ArtaProcessNormal CreateArtaProcessN(Normal normal, double[] artaCorrelationCoefficients, RandomSource random)
        {
            ArtaProcessNormal arta = null;
            ArProcess ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);
            arta = new ArtaProcessNormal(ar, normal.Mean, normal.Variance);
            return arta;
        }

        private static ArtaProcessUniform CreateArtaProcessU(ContinuousUniform uniform, double[] artaCorrelationCoefficients, RandomSource random)
        {
            ArtaProcessUniform arta = null;
            int dim = artaCorrelationCoefficients.Length;
            double[] arCorrelationCoefficients = new double[dim];
            for(int i= 0; i < dim; i++)
            {
                arCorrelationCoefficients[i] = 2 * System.Math.Sin(System.Math.PI * artaCorrelationCoefficients[i] / 6);
            }
            ArProcess ar = ArProcessFactory.CreateArProcess(arCorrelationCoefficients, random);
            arta = new ArtaProcessUniform(ar, uniform.LowerBound, uniform.UpperBound);
            return arta;
        }
    }
}
