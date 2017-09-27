/*******************************************************************************
 * Copyright 2013 by Tobias Uhlig
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 ******************************************************************************/
package de.unibw.jarta.tests;

import org.apache.commons.math3.analysis.UnivariateFunction;
import org.apache.commons.math3.analysis.integration.IterativeLegendreGaussIntegrator;
import org.apache.commons.math3.analysis.integration.UnivariateIntegrator;
import org.apache.commons.math3.distribution.ExponentialDistribution;
import org.apache.commons.math3.distribution.NormalDistribution;
import org.apache.commons.math3.distribution.RealDistribution;
import org.apache.commons.math3.distribution.TDistribution;
import org.apache.commons.math3.distribution.UniformRealDistribution;

public class FeasibilityTest {

	private final UnivariateIntegrator	integrator;
	private final double				minimumFeasibleBivariateCorrelation;



	public FeasibilityTest(UnivariateIntegrator integrator, RealDistribution distribution) {
		this.integrator = integrator;
		this.minimumFeasibleBivariateCorrelation = calculateMinimumFeasibleBivariateCorrelation(distribution);
	}



	public FeasibilityTest(RealDistribution distribution) {
		this(new IterativeLegendreGaussIntegrator(5, IterativeLegendreGaussIntegrator.DEFAULT_RELATIVE_ACCURACY, IterativeLegendreGaussIntegrator.DEFAULT_ABSOLUTE_ACCURACY), distribution);
	}



	public double getMinimumFeasibleBivariateCorrelation() {
		return minimumFeasibleBivariateCorrelation;
	}



	public void checkFeasibility(double[] artaCorrelations) {
		for (double r : artaCorrelations) {
			if (!isFeasible(r)) {
				throw new NonFeasibleCorrelationException(r, getMinimumFeasibleBivariateCorrelation(), 1.0);
			}
		}
	}



	public boolean isFeasible(double correlation) {
		boolean feasible = true;
		if (getMinimumFeasibleBivariateCorrelation() > correlation || correlation > 1.0) {
			feasible = false;
		}
		return feasible;
	}



	private double calculateMinimumFeasibleBivariateCorrelation(RealDistribution distribution) {
		double minFBC = 0.0;

		// consider special cases, final else is general approach
		if (distribution instanceof NormalDistribution || distribution instanceof UniformRealDistribution || distribution instanceof TDistribution) {
			minFBC = -1.0;
		} else if (distribution instanceof ExponentialDistribution) {
			minFBC = 1.0 - Math.PI * Math.PI / 6.0;
		} else {
			double mean = distribution.getNumericalMean();
			double variance = distribution.getNumericalVariance();
			double integral = integrator.integrate(IterativeLegendreGaussIntegrator.DEFAULT_MAX_ITERATIONS_COUNT, new UnivariatDistributionFunction(distribution), 0.0, 1.0);
			minFBC = (integral - mean * mean) / variance;
		}

		return minFBC;
	}

	private class UnivariatDistributionFunction implements UnivariateFunction {
		private final RealDistribution	distribution;



		private UnivariatDistributionFunction(RealDistribution distribution) {
			this.distribution = distribution;
		}



		@Override
		public double value(double arg0) {
			double result = distribution.inverseCumulativeProbability(arg0) * distribution.inverseCumulativeProbability(1 - arg0);
			return result;
		}
	}
}
