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


import org.apache.commons.math3.distribution.NormalDistribution;
import org.apache.commons.math3.distribution.RealDistribution;

import de.unibw.jarta.math.BivariateStandardNormal;
import de.unibw.jarta.util.LruCache;

/**
 * Provides methods to estimate the expected autocorrelations of an ARTA process
 * given autocorrelations of the underlying autoregressive process and the
 * distribution used in the ARTA process. The estimation relies on the 
 * Gauss-Kronrod quadrature rule.
 * 
 * @author tobias uhlig
 * 
 */
public class ArtaCorrelationEstimator {
	
	/** Abscissae used for the Gauss and Kronrod weights in interval -1.0 to 1.0 */
	private final double[] abscissae =         { 0.991455371120812639206854697526329,
												0.949107912342758524526189684047851,
												0.864864423359769072789712788640926,
												0.741531185599394439863864773280788,
												0.586087235467691130294144838258730,
												0.405845151377397166906606412076961,
												0.207784955007898467600689403773245,
												0.0,
											   -0.207784955007898467600689403773245,
											   -0.405845151377397166906606412076961,
											   -0.586087235467691130294144838258730,
											   -0.741531185599394439863864773280788,
											   -0.864864423359769072789712788640926,
									   		   -0.949107912342758524526189684047851,
									   		   -0.991455371120812639206854697526329};
	
	/** The Kronrod weights corresponding to the defined abscissae */ 
	private final double[] kronrodWeights =   { 0.022935322010529224963732008058970,
												0.063092092629978553290700663189204,
												0.104790010322250183839876322541518,
												0.140653259715525918745189590510238,
												0.169004726639267902826583426598550,
												0.190350578064785409913256402421014,
												0.204432940075298892414161999234649,
												0.209482141084727828012999174891714,
												0.204432940075298892414161999234649,
												0.190350578064785409913256402421014,
												0.169004726639267902826583426598550,
												0.140653259715525918745189590510238,
												0.104790010322250183839876322541518,
												0.063092092629978553290700663189204,
												0.022935322010529224963732008058970};
	
	/** The Gauss weights corresponding to the defined abscissae */ 
	private final double[] gaussWeights = 	  { 0.0,
												0.129484966168869693270611432679082,
												0.0,
												0.279705391489276667901467771423780,
												0.0,
												0.381830050505118944950369775488975,
												0.0,
												0.417959183673469387755102040816327,
												0.0,
												0.381830050505118944950369775488975,
												0.0,
												0.279705391489276667901467771423780,
												0.0,
												0.129484966168869693270611432679082,
												0.0};

	private final RealDistribution			distribution;
	private final NormalDistribution		standardNormal				= new NormalDistribution();
	private final LruCache<Double, Double> 	estimationsCache			= new LruCache<Double, Double>(100);
	private final LruCache<Double, Double> 	transformationCache			= new LruCache<Double, Double>(1000);

	private static final double TOLERANCE_OUTER = 0.00001;
	private static final double TOLERANCE_INNER = 0.00005;
	

	/**
	 * Generates a new estimator for autocorrelations of an ARTA-Process
	 * modeling the given marginal distribution.
	 * 
	 * @param distribution
	 *            the marginal distribution modeled the assumed ARTA-Process.
	 */
	public ArtaCorrelationEstimator(RealDistribution distribution) {
		this.distribution = distribution;
	}



	/**
	 * Given an autocorrelation value of an autoregressive process, estimates
	 * the resulting autocorrelation for the ARTA process.
	 * 
	 * @param arAutocorrelation
	 *            the autocorrelation of the autoregressive process
	 * @return the estimated autocorrelation for the ARTA process
	 */
	public double estimateArtaCorrelation(double arAutocorrelation) {
		Double result = estimationsCache.get(arAutocorrelation);
		if (result == null) {
			double e        = integrate(-8, 8, arAutocorrelation);
			double mean     = distribution.getNumericalMean();
			double variance = distribution.getNumericalVariance();
			result 			= (e - mean * mean) / variance;
			estimationsCache.put(arAutocorrelation, result);
		}
		return result;
	}



	private double integrate(double from, double to, double rho) {
		double kronrodSum   = 0.0;
		double gaussSum 	= 0.0;
		double x 			= 0.0;
		double xt 			= 0.0;
		double center 		= (from + to) / 2.0;
		double halfDistance = to - center;

		for (int i = 0; i < abscissae.length; i++) {
			// linear mapping
			x = abscissae[i] * halfDistance + center;
			// transformation
			xt = transform(x);
			kronrodSum += xt * integrateInner(-8, 8, rho, x) * kronrodWeights[i];
			gaussSum   += xt * integrateInner(-8, 8, rho, x) * gaussWeights[i];
		}

		kronrodSum = halfDistance * kronrodSum;
		gaussSum   = halfDistance * gaussSum;

		if (Math.abs(kronrodSum - gaussSum) > TOLERANCE_OUTER) {
			kronrodSum = integrate(from, center, rho) + integrate(center, to, rho);
		}

		return kronrodSum;
	}



	private double integrateInner(double from, double to, double rho, double x) {
		double kronrodSum 	= 0.0;
		double gaussSum 	= 0.0;
		double y 			= 0.0;
		double yt 			= 0.0;
		double densityBN	= 0.0;
		double center	 	= (from + to) / 2.0;
		double halfDistance = to - center;

		for (int i = 0; i < abscissae.length; i++) {
			y 			= abscissae[i] * halfDistance + center;
			yt 			= transform(y);
			densityBN 	= BivariateStandardNormal.getDensity(x, y, rho);
			kronrodSum += yt * densityBN * kronrodWeights[i];
			gaussSum   += yt * densityBN * gaussWeights[i];
		}

		kronrodSum = halfDistance * kronrodSum;
		gaussSum   = halfDistance * gaussSum;

		if (Math.abs(kronrodSum - gaussSum) > TOLERANCE_INNER) {
			kronrodSum = integrateInner(from, center, rho, x) + integrateInner(center, to, rho, x);
		}
		
		return kronrodSum;
	}



	private double transform(double value) {
		Double result = transformationCache.get(value);
		if (result == null) {
			// Transform to uniform distribution U(0,1)
			result = standardNormal.cumulativeProbability(value);
			// Transform to desired distribution
			result = distribution.inverseCumulativeProbability(result);
			transformationCache.put(value, result);
		}
		
		return result;
	}



	



}
