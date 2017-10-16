using ARTA.core.ch.hsr.fitting;
using ARTA.core.ch.hsr.math;
using ARTA.core.ch.hsr.test;
using Math3.distribution;
using Math3.linear;
using Math3.random;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    class ArtaProcessFactory
    {
        public const double DEFAULT_ERROR = 0.0001;
        private ArtaProcessFactory()
        {
            // no instantiation

        }
        public static ArtaProcess CreateArtaProcess(double[] data)// throws NonFeasibleCorrelationException, NotStationaryException 
        {

            EmpiricalDistribution distribution = new EmpiricalDistribution(data);
            int order = OrderEstimator.EstimateOrder(data);
            Console.WriteLine("order" + order);
            double[] artaCorrelationCoefficients = Array.Copy(AutoCorrelation.CalculateAcfs(data, order), 1, order + 1);
            return CreateArtaProcess(distribution, artaCorrelationCoefficients, new RandomAdaptor(new Well19937c()));
        }

        public static IArtaProcess CreateArtaProcess(RealDistribution distribution, double[] artaCorrelationCoefficients)// throws NonFeasibleCorrelationException, NotStationaryException 
        {
            return CreateArtaProcess(distribution, artaCorrelationCoefficients, new RandomAdaptor(new Well19937c()));
        }

        public static IArtaProcess CreateArtaProcess(RealDistribution distribution, double[] artaCorrelationCoefficients, RandomGenerator random) //throws NonFeasibleCorrelationException, NotStationaryException 
        {
            AbstractArtaProcess arta = null;
            if (artaCorrelationCoefficients == null || artaCorrelationCoefficients.Length == 0)
            {
                double[] noCorrelation = { 0.0 };
                artaCorrelationCoefficients = noCorrelation;
            }

            // check feasibility 
            FeasibilityTest ft = new FeasibilityTest(distribution);
            ft.CheckFeasibility(artaCorrelationCoefficients);

            // check if correlation matrix is positive definite, if not CholeskyDecomposition throws Error
            new CholeskyDecomposition(AutoCorrelation.GetCorrelationMatrix(artaCorrelationCoefficients));

            if (distribution is NormalDistribution)
            {
                arta = CreateArtaProcessN((NormalDistribution)distribution, artaCorrelationCoefficients, random);
            }
            else if (distribution is UniformRealDistribution)
            {
                arta = createArtaProcessU((UniformRealDistribution)distribution, artaCorrelationCoefficients, random);
            }
            else
            {
                arta = CreateArtaProcessG(distribution, artaCorrelationCoefficients, random);
            }
            return arta;
        }



        private static ArtaProcessGeneral CreateArtaProcessG(RealDistribution distribution, double[] artaCorrelationCoefficients, RandomGenerator random) //throws NotStationaryException
        {
            ArtaProcessGeneral arta = null;
            AutocorrelationFitter fitter = new AutocorrelationFitter(distribution);
            double[] arCorrelationCoefficients = fitter.FitArAutocorrelations(artaCorrelationCoefficients, DEFAULT_ERROR);
            ArProcess ar = ArProcessFactory.CreateArProcess(arCorrelationCoefficients, random);
            arta = new ArtaProcessGeneral(ar, distribution);
            return arta;
        }



        private static ArtaProcessNormal CreateArtaProcessN(NormalDistribution normal, double[] artaCorrelationCoefficients, RandomGenerator random)// throws NotStationaryException
        {
            ArtaProcessNormal arta = null;
            // By definition: arCorrelationCoefficients == artaCorrelationCoefficients
            ArProcess ar = ArProcessFactory.CreateArProcess(artaCorrelationCoefficients, random);
            arta = new ArtaProcessNormal(ar, normal.getNumericalMean(), normal.getNumericalVariance());
            return arta;
        }



        private static ArtaProcessUniform createArtaProcessU(UniformRealDistribution uniform, double[] artaCorrelationCoefficients, RandomGenerator random) //throws NotStationaryException
        {
            ArtaProcessUniform arta = null;
            int dim = artaCorrelationCoefficients.Length;
            double[] arCorrelationCoefficients = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                arCorrelationCoefficients[i] = 2 * Math.Sin(Math.PI * artaCorrelationCoefficients[i] / 6);
            }
            ArProcess ar = ArProcessFactory.CreateArProcess(arCorrelationCoefficients, random);
            arta = new ArtaProcessUniform(ar, uniform.getSupportLowerBound(), uniform.getSupportUpperBound());
            return arta;
        }
    }
}
