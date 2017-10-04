using ARTA_Libary.ch.hsr.artac.fitting;
using ARTA_Libary.ch.hsr.artac.math;
using ARTA_Libary.ch.hsr.artac.tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.arta
{
    internal class ArtaProcessFactory
    {
        private ArtaProcessFactory()
        {
            // no instantiation

        }

        public static ArtaProcess CreateArtaProcess(double[] data)
        {
            ArtaProcess artaProcess = null;
            try
            {

                EmpiricalDistribution distribution = new EmpiricalDistribution(data);
                int order = OrderEstimator.estimateOrder(data);
                Console.WriteLine("order" + order);
                double[] artaCorrelationCoefficients = Arrays.copyOfRange(AutoCorrelation.calculateAcfs(data, order), 1, order + 1);
                Console.WriteLine("accs" + Arrays.toString(artaCorrelationCoefficients));
                artaProcess = createArtaProcess(distribution, artaCorrelationCoefficients, new RandomAdaptor(new MersenneTwister()));
            }
            catch (NonFeasibleCorrelationException e)
            {

            }
            catch (NotStationaryException e)
            {

            }

            return artaProcess;
        }

        public static ArtaProcess createArtaProcess(RealDistribution distribution, double[] artaCorrelationCoefficients) throws NonFeasibleCorrelationException, NotStationaryException {
		return createArtaProcess(distribution, artaCorrelationCoefficients, new RandomAdaptor(new MersenneTwister()));
	}


    /**
     * 
     * @param distribution
     * @param artaCorrelationCoefficients
     * @param random
     * @return
     * @throws NonFeasibleCorrelationException
     * @throws NotStationaryException
     */
    public static ArtaProcess createArtaProcess(RealDistribution distribution, double[] artaCorrelationCoefficients, RandomGenerator random) throws NonFeasibleCorrelationException, NotStationaryException {
		AbstractArtaProcess arta = null;
		if (artaCorrelationCoefficients == null || artaCorrelationCoefficients.length == 0) {
			double[] noCorrelation = { 0.0 };
    artaCorrelationCoefficients = noCorrelation;
		}


// check feasibility 
FeasibilityTest ft = new FeasibilityTest(distribution);
ft.checkFeasibility(artaCorrelationCoefficients);
		
		// check if correlation matrix is positive definite, if not CholeskyDecomposition throws Error
		new CholeskyDecomposition(AutoCorrelation.getCorrelationMatrix(artaCorrelationCoefficients));

		if (distribution instanceof NormalDistribution) {
			arta = createArtaProcessN((NormalDistribution) distribution, artaCorrelationCoefficients, random);
		} else if (distribution instanceof UniformRealDistribution) {
			arta = createArtaProcessU((UniformRealDistribution) distribution, artaCorrelationCoefficients, random);
		} else {
			arta = createArtaProcessG(distribution, artaCorrelationCoefficients, random);
		}
		return arta;
	}



	private static ArtaProcessGeneral createArtaProcessG(RealDistribution distribution, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException
{
    ArtaProcessGeneral arta = null;
    AutocorrelationFitter fitter = new AutocorrelationFitter(distribution);
double[] arCorrelationCoefficients = fitter.fitArAutocorrelations(artaCorrelationCoefficients, DEFAULT_ERROR);
ArProcess ar = ArProcessFactory.createArProcess(arCorrelationCoefficients, random);
arta = new ArtaProcessGeneral(ar, distribution);
		return arta;
	}



	private static ArtaProcessNormal createArtaProcessN(NormalDistribution normal, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException
{
    ArtaProcessNormal arta = null;
    // By definition: arCorrelationCoefficients == artaCorrelationCoefficients
    ArProcess ar = ArProcessFactory.createArProcess(artaCorrelationCoefficients, random);
    arta = new ArtaProcessNormal(ar, normal.getNumericalMean(), normal.getNumericalVariance());
		return arta;
	}



	private static ArtaProcessUniform createArtaProcessU(UniformRealDistribution uniform, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException
{
    ArtaProcessUniform arta = null;
		int dim = artaCorrelationCoefficients.length;
		double[]
    arCorrelationCoefficients = new double[dim];
		for (int i = 0; i<dim; i++) {
			arCorrelationCoefficients[i] = 2 * Math.sin(Math.PI* artaCorrelationCoefficients[i] / 6);
		}
		ArProcess ar = ArProcessFactory.createArProcess(arCorrelationCoefficients, random);
arta = new ArtaProcessUniform(ar, uniform.getSupportLowerBound(), uniform.getSupportUpperBound());
		return arta;
	}
    }
}
