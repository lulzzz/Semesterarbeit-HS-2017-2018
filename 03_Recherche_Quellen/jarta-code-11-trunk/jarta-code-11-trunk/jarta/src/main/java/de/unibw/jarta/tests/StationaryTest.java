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


public class StationaryTest {
	
	
	/**
	 * Checks if the given coefficients (alphas) for an autoregressive process
	 * lead to a stationary AR process.
	 * 
	 * @param alphas
	 *            given coefficients for an autoregressive process
	 */
	public final static boolean isStationary(double[] alphas) {
		
		int dim = alphas.length;
		double[] a = new double[dim+1];
		double[] b = new double[dim+1];
		
		a[0] = -1.0;
		for (int i = 0; i < dim; i++) {
			a[i+1]  = alphas[i];
		}
		
		for (int k = dim; k > 0 ;k--){
			for (int j = 0; j < k; j++) {				
				b[j] = a[0] * a[j] - a[k] * a[k-j];
				
			}
			if (b[0] <= 0.0) {
				//process is not stationary
				return false;
			}
			
			for (int i = 0; i < dim + 1; i++) {
				a[i] = b[i];
			}	
		}
		//process is stationary 
		return true;
	} 
	
	
}
