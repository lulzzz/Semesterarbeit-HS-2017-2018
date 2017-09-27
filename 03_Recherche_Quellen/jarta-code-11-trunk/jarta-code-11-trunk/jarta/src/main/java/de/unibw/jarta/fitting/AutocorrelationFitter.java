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
package de.unibw.jarta.fitting;

import org.apache.commons.math3.distribution.RealDistribution;

public class AutocorrelationFitter {
	private final ArtaCorrelationEstimator	artaCorrelationEstimator;



	public AutocorrelationFitter(RealDistribution distribution) {
		// TODO use other Estimator for empirical distribution
		this.artaCorrelationEstimator = new ArtaCorrelationEstimator(distribution);
	}



	public double[] fitArAutocorrelations(double[] desiredArtaAutocorrelations, double maxError) {
		int dim = desiredArtaAutocorrelations.length;
		double[] maxErrors = new double[dim];
		for (int i = 0; i < dim; i++) {
			maxErrors[i] = maxError;
		}
		return fitArAutocorrelations(desiredArtaAutocorrelations, maxErrors);
	}



	public double[] fitArAutocorrelations(double[] desiredArtaAutocorrelations, double[] maxErrors) {
		// TODO error handling
		int dim = desiredArtaAutocorrelations.length;
		double[] result = new double[dim];
		for (int i = 0; i < dim; i++) {
			result[i] = fitArAutocorrelation(desiredArtaAutocorrelations[i], maxErrors[i]);
		}
		return result;
	}



	public double fitArAutocorrelation(double desiredArtaAutocorrelation, double maxError) {
		// TODO error handling
		double lowerBound  			= -1.0; // correlation cannot be smaller than -1.0
		double upperBound  			= 1.0;	// correlation cannot be bigger than 1.0
		double centerPoint;					
		double estimatedCorrelation; 
		
		// Check if lower bound is an adequate fit
		estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(lowerBound);
		if (Math.abs(desiredArtaAutocorrelation - estimatedCorrelation) < maxError) {
			return lowerBound;
		}
		
		// Check if upper bound is an adequate fit
		estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(upperBound);
		if (Math.abs(desiredArtaAutocorrelation - estimatedCorrelation) < maxError) {
			return upperBound;
		}
		
		/*
		 *  1. calculate center point and estimate correlation at center point
		 *  2. update bounds to reflect new search interval
		 *  3. if estimated correlation is not a good fit return to step 1  
		 */
		do  {
			centerPoint = (lowerBound + upperBound) / 2.0;
			estimatedCorrelation = artaCorrelationEstimator.estimateArtaCorrelation(centerPoint);
			if (desiredArtaAutocorrelation < estimatedCorrelation) {
				upperBound = centerPoint;
			} else {
				lowerBound = centerPoint;
			}	
		} while (Math.abs(desiredArtaAutocorrelation - estimatedCorrelation) > maxError);
		
		return centerPoint;
	}
	
}
