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

import de.unibw.jarta.util.DoubleHistory;
import de.unibw.jarta.util.ValueHistory;


/**
 * Models a autoregressive process, with order p : <p>
 *  
 * Z_t = a_1 * Z_t-1 + a_2 * Z_t-2 + ... + a_p * Z_t-p + epsilon <p>
 * 
 * epsilon is a value from white-noise process - N(0,1)<p>
 * a_n are the coefficients the autoregressive process
 */

public class ArProcess {

	private final double[] alphas;
	private final ValueHistory<Double> vals;
	private final NormalDistribution whiteNoiseProcess;
	

	/**
	 * Generates a new autoregressive process with the given parameters. The order of
	 * the process is given by the dimension of the array containing the AR-coefficients (alpha).
	 * 
	 * @param alphas an array containing the AR-coefficients
	 * @param variance the variance for white-noise process
	 * @param random a random generator used for the white-noise process
	 */
	 public ArProcess(double[] alphas, NormalDistribution whiteNoiseProcess) {
		this.alphas = alphas;
		this.vals = new DoubleHistory(alphas.length);
		this.whiteNoiseProcess = whiteNoiseProcess;
	}


	/**
	 * Generates the next value of the autoregressive process.
	 * @return the next value of the autoregressive process
	 */
	public double next() {
		double value = whiteNoiseProcess.sample();
		for (int i = 0; i < alphas.length; i++) {
			value = value + alphas[i] * vals.get(i);
		}
		vals.add(value);
		return value;
	}
	
	@Override
	public String toString() {
		return "AR-Process \n alphas	= " +
				Arrays.toString(alphas) + "\n variance	=" +
				(whiteNoiseProcess.getNumericalVariance());
	}
	
}
