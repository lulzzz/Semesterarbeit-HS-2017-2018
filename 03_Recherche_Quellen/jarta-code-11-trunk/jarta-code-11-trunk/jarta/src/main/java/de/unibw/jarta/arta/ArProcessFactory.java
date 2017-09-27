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

import org.apache.commons.math3.distribution.NormalDistribution;
import org.apache.commons.math3.linear.Array2DRowRealMatrix;
import org.apache.commons.math3.linear.CholeskyDecomposition;
import org.apache.commons.math3.linear.RealMatrix;
import org.apache.commons.math3.random.MersenneTwister;
import org.apache.commons.math3.random.RandomGenerator;
import org.apache.commons.math3.util.FastMath;

import de.unibw.jarta.math.AutoCorrelation;
import de.unibw.jarta.tests.NotStationaryException;
import de.unibw.jarta.tests.StationaryTest;

public class ArProcessFactory {

	private ArProcessFactory() {
		// no instantiation
	}



	/**
	 * Generates a stationary autoregressive process with the given
	 * autocorrelations, using a Mersenne-Twister to generate the white-noise
	 * for the AR-process. The marginal distribution of the resulting process is
	 * N(0,1) - normal distributed with mean 0.0 and variance 1.0.
	 * 
	 * @param arAutocorrelations
	 * @return the resulting autoregressive process
	 * @throws NotStationaryException
	 *             if the resulting process would not be stationary
	 */
	public final static ArProcess createArProcess(double[] arAutocorrelations) throws NotStationaryException {
		return createArProcess(arAutocorrelations, new MersenneTwister());
	}



	/**
	 * Generates a stationary autoregressive process with the given
	 * autocorrelations and using the given Random to generate the white-noise
	 * for the AR-process. The marginal distribution of the resulting process is
	 * N(0,1) - normal distributed with mean 0.0 and variance 1.0.
	 * 
	 * @param arAutocorrelations
	 * @param random
	 *            a random number generator
	 * @return the resulting autoregressive process
	 * @throws NotStationaryException
	 *             if the resulting process would not be stationary
	 */
	public final static ArProcess createArProcess(double[] arAutocorrelations, RandomGenerator random) throws NotStationaryException {
		double[] alphas = arAutocorrelationsToAlphas(arAutocorrelations);
		if (!StationaryTest.isStationary(alphas)) {
			throw new NotStationaryException();
		}
		double variance = calculateVariance(arAutocorrelations, alphas);
		//TODO check if variance is positive? otherwise exception
		NormalDistribution whiteNoiseProcess = new NormalDistribution(random, 0.0, FastMath.sqrt(variance), NormalDistribution.DEFAULT_INVERSE_ABSOLUTE_ACCURACY);
		
		return new ArProcess(alphas, whiteNoiseProcess);
	}

	

	/**
	 * Determines the coefficients (alpha) for the autoregressive process. An
	 * autoregressive process using the calculated alphas will have the
	 * autocorrelation structure given as input. Alphas are calculated by
	 * solving the Yule-Walker equations.
	 * 
	 * @param arAutocorrelations
	 *            the desired autocorrelation for an autoregressive process.
	 * @return the coefficients (alpha) for the autoregressive process
	 */
	public final static double[] arAutocorrelationsToAlphas(double[] arAutocorrelations) {
		int dim = arAutocorrelations.length;
		double[] alphas = new double[dim];

		RealMatrix psi = AutoCorrelation.getCorrelationMatrix(arAutocorrelations);
		RealMatrix r = new Array2DRowRealMatrix(arAutocorrelations).transpose();
		RealMatrix a = r.multiply(new CholeskyDecomposition(psi).getSolver().getInverse());
		alphas = a.getRow(0);
		return alphas;
	}



	/**
	 * Calculates the variances for the white-noise process. It is calculated
	 * using the autocorrelations and the alpha coefficients of the
	 * autoregressive process. Using the calculated variance for the white-noise
	 * process will result in an autoregressive process with a variance of one
	 * (sigma^2(AR) = 1.0).
	 * 
	 * @param arAutocorrelations
	 *            the autocorrelations for the autoregressive process
	 * @param alphas
	 *            coefficients for the autoregressive process
	 * @return the varinace for the white-noise process.
	 */
	public final static double calculateVariance(double[] arAutocorrelations, double[] alphas) {
		double variance = 1.0;
		for (int i = 0; i < alphas.length; i++) {
			variance = variance - alphas[i] * arAutocorrelations[i];
		}
		return variance;
	}
	


}
