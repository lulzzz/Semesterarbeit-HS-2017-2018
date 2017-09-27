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

public class BivariateStandardNormal {

	private BivariateStandardNormal() {
		// no instantiation
	}



	/**
	 * Calculates the density of the bivariate standard normal distribution at
	 * the point (x, y) with a given correlation rho between x and y.
	 * 
	 * @param x
	 *            the x value
	 * @param y
	 *            the y value
	 * @param rho
	 *            correlation between x and y
	 * @return density of the bivariate standard normal distribution
	 */
	public final static double getDensity(double x, double y, double rho) {
		double d = 1.0 - rho * rho;
		return Math.exp(-(x * x - 2.0 * rho * x * y + y * y) / (2 * d)) / (2 * Math.PI * Math.sqrt(d));
	}
}
