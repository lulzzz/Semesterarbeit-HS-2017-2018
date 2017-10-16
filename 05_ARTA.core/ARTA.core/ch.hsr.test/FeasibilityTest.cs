using Math3.analysis;
using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.test
{
    class FeasibilityTest
    {

        private readonly UnivariateIntegrator  integrator;
	private readonly double minimumFeasibleBivariateCorrelation;



        public FeasibilityTest(UnivariateIntegrator integrator, RealDistribution distribution)
        {
            this.integrator = integrator;
            this.minimumFeasibleBivariateCorrelation = CalculateMinimumFeasibleBivariateCorrelation(distribution);
        }



        public FeasibilityTest(RealDistribution distribution)
        {
            this(new IterativeLegendreGaussIntegrator(5, IterativeLegendreGaussIntegrator.DEFAULT_RELATIVE_ACCURACY, IterativeLegendreGaussIntegrator.DEFAULT_ABSOLUTE_ACCURACY), distribution);
        }



        public double getMinimumFeasibleBivariateCorrelation()
        {
            return minimumFeasibleBivariateCorrelation;
        }



        public void CheckFeasibility(double[] artaCorrelations)
        {
            for (double r : artaCorrelations)
            {
                if (!IsFeasible(r))
                {
                    throw new NonFeasibleCorrelationException(r, getMinimumFeasibleBivariateCorrelation(), 1.0);
                }
            }
        }



        public bool IsFeasible(double correlation)
        {
            bool feasible = true;
            if (getMinimumFeasibleBivariateCorrelation() > correlation || correlation > 1.0)
            {
                feasible = false;
            }
            return feasible;
        }



        private double CalculateMinimumFeasibleBivariateCorrelation(RealDistribution distribution)
        {
            double minFBC = 0.0;

            // consider special cases, final else is general approach
            if (distribution is NormalDistribution || distribution is UniformRealDistribution || distribution is TDistribution) {
                minFBC = -1.0;
            } else if (distribution is ExponentialDistribution) {
                minFBC = 1.0 - Math.PI * Math.PI / 6.0;
            } else {
                double mean = distribution.getNumericalMean();
                double variance = distribution.getNumericalVariance();
                double integral = integrator.integrate(IterativeLegendreGaussIntegrator.DEFAULT_MAX_ITERATIONS_COUNT, new UnivariatDistributionFunction(distribution), 0.0, 1.0);
                minFBC = (integral - mean * mean) / variance;
            }

            return minFBC;
        }

        private class UnivariatDistributionFunction : UnivariateFunction
        {

        private readonly RealDistribution  distribution;



		private UnivariatDistributionFunction(RealDistribution distribution)
        {
            this.distribution = distribution;
        }

        public override double Value(double d)
        {
            double result = distribution.inverseCumulativeProbability(d) * distribution.inverseCumulativeProbability(1 - d);
            return result;
        }
    }
}
}
