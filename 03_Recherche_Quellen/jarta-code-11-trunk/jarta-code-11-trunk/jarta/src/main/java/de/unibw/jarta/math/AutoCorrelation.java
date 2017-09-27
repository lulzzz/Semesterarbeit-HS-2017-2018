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
package de.unibw.jarta.math;

import java.util.Arrays;

import org.apache.commons.math3.linear.Array2DRowRealMatrix;
import org.apache.commons.math3.linear.RealMatrix;
import org.apache.commons.math3.stat.correlation.PearsonsCorrelation;

public class AutoCorrelation {

	private AutoCorrelation() {
		// no instantiation
	}

	private final static PearsonsCorrelation	pearsonsAc	= new PearsonsCorrelation();



	/**
	 * Calculates the autocorrelation for a certain lag using pearsons
	 * correlation coefficient.
	 * 
	 * @param data
	 *            the sample values to evaluate
	 * @param lag
	 *            the offset used for the autocorrelation
	 * @return autocorrelation coefficient at given lag
	 */
	public final static double calculateAcf(double[] data, int lag) {
		double acc = 0.0;
		int l = data.length;
		if (lag < 0)
			throw new IllegalArgumentException("Negative lags are not allowed");
		if (lag > l)
			throw new IllegalArgumentException("Lag exceeds sample size");

		if (lag == 0) {
			acc = 1.0;
		} else {
			// TODO consider faster implementation without array-copying
			acc = pearsonsAc.correlation(Arrays.copyOfRange(data, 0, l - lag), Arrays.copyOfRange(data, lag, l));
		}
		return acc;
	}



	/**
	 * Calculates all autocorrelations coefficients (Pearson) up to a given
	 * maximal lag.
	 * 
	 * @param data
	 *            the sample values to evaluate
	 * @param maxLag
	 *            the maximal lag to consider.
	 * @return an array containing the autocorrelation coefficients
	 */
	public final static double[] calculateAcfs(double[] data, int maxLag) {
		double[] accs = new double[maxLag + 1];
		for (int lag = 0; lag <= maxLag; lag++) {
			accs[lag] = calculateAcf(data, lag);
		}
		return accs;
	}


	/**
	 * 
	 * @param autocorrelations
	 * @return
	 */
	public final static RealMatrix getCorrelationMatrix(double[] autocorrelations) {
		RealMatrix result = null;
		int dim = autocorrelations.length;
		double[][] psiValues = new double[dim][dim];

		for (int row = 0; row < dim; row++) {
			for (int col = row; col < dim; col++) {
				if (row == col) {
					psiValues[col][row] = 1.0;
				} else {
					psiValues[col][row] = autocorrelations[col - row - 1];
					psiValues[row][col] = autocorrelations[col - row - 1];
				}

			}
		}

		result = new Array2DRowRealMatrix(psiValues);
		return result;
	}
	
	
	public final static double[] calculatePacfs(double[] acfs) {
		int size = acfs.length;
		double[] 	pacfs = new double[size];
		double[][] 	A = new double[size][size];
		double[] 	V = new double[size];
		A[0][0] = acfs[0];
		A[1][1] = acfs[1] / acfs[0];
		V[1]	= A[1][1] / acfs[1];
		for (int k = 2; k < size; k++) {
			double sum = 0;
			for (int j = 1; j < k; j++) {
				sum += A[j][j] * acfs[k-j];
			}
			A[k][k] = (acfs[k] - sum) / V[k-1];
			
			for (int j = 1; j <k; j++) {
				A[j][k] = A[j][k-1] - A[k][k] * A[k-j][k-1];
			}
			V[k] = V[k-1] * (1 - A[k][k] * A[k][k]);
			
		}
		
		for(int i = 0; i < size; i++) {
			pacfs[i] = A[i][i];
		}
		return pacfs;
	}
	
}
