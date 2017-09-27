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
package de.unibw.jarta.arta;

import java.util.Arrays;

import org.apache.commons.math3.distribution.NormalDistribution;
import org.apache.commons.math3.distribution.RealDistribution;
import org.apache.commons.math3.distribution.UniformRealDistribution;
import org.apache.commons.math3.linear.CholeskyDecomposition;
import org.apache.commons.math3.random.MersenneTwister;
import org.apache.commons.math3.random.RandomAdaptor;
import org.apache.commons.math3.random.RandomGenerator;

import de.unibw.jarta.fitting.AutocorrelationFitter;
import de.unibw.jarta.fitting.OrderEstimator;
import de.unibw.jarta.math.AutoCorrelation;
import de.unibw.jarta.math.EmpiricalDistribution;
import de.unibw.jarta.tests.NonFeasibleCorrelationException;
import de.unibw.jarta.tests.FeasibilityTest;
import de.unibw.jarta.tests.NotStationaryException;

public class ArtaProcessFactory {

	
	public final static double	DEFAULT_ERROR	= 0.0001;



	private ArtaProcessFactory() {
		// no instantiation

	}

	public static ArtaProcess createArtaProcess(double[] data) throws NonFeasibleCorrelationException, NotStationaryException {
		EmpiricalDistribution distribution = new EmpiricalDistribution(data);
		int order = OrderEstimator.estimateOrder(data);	
		System.out.println("order" + order);
		double[] artaCorrelationCoefficients = Arrays.copyOfRange(AutoCorrelation.calculateAcfs(data, order), 1, order + 1);		
		System.out.println("accs" + Arrays.toString(artaCorrelationCoefficients));
		return createArtaProcess(distribution, artaCorrelationCoefficients, new RandomAdaptor(new MersenneTwister()));
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
			double[] noCorrelation = {0.0};
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



	private static ArtaProcessGeneral createArtaProcessG(RealDistribution distribution, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException {
		ArtaProcessGeneral arta = null;
		AutocorrelationFitter fitter = new AutocorrelationFitter(distribution);
		double[] arCorrelationCoefficients = fitter.fitArAutocorrelations(artaCorrelationCoefficients, DEFAULT_ERROR);
		ArProcess ar = ArProcessFactory.createArProcess(arCorrelationCoefficients, random);
		arta = new ArtaProcessGeneral(ar, distribution);
		return arta;
	}



	private static ArtaProcessNormal createArtaProcessN(NormalDistribution normal, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException {
		ArtaProcessNormal arta = null;
		// By definition: arCorrelationCoefficients == artaCorrelationCoefficients
		ArProcess ar = ArProcessFactory.createArProcess(artaCorrelationCoefficients, random);
		arta = new ArtaProcessNormal(ar, normal.getNumericalMean(), normal.getNumericalVariance());
		return arta;
	}



	private static ArtaProcessUniform createArtaProcessU(UniformRealDistribution uniform, double[] artaCorrelationCoefficients, RandomGenerator random) throws NotStationaryException {
		ArtaProcessUniform arta = null;
		int dim = artaCorrelationCoefficients.length;
		double[] arCorrelationCoefficients = new double[dim];
		for (int i = 0; i < dim; i++) {
			arCorrelationCoefficients[i] = 2 * Math.sin(Math.PI * artaCorrelationCoefficients[i] / 6);
		}
		ArProcess ar = ArProcessFactory.createArProcess(arCorrelationCoefficients, random);
		arta = new ArtaProcessUniform(ar, uniform.getSupportLowerBound(), uniform.getSupportUpperBound());
		return arta;
	}

}
