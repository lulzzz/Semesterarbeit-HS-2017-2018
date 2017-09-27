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
package de.unibw.jarta.examples;

import java.util.Arrays;

import org.apache.commons.math3.distribution.ExponentialDistribution;
import org.apache.commons.math3.distribution.RealDistribution;
import org.apache.commons.math3.stat.StatUtils;

import de.unibw.jarta.arta.ArtaProcess;
import de.unibw.jarta.arta.ArtaProcessFactory;
import de.unibw.jarta.math.AutoCorrelation;
import de.unibw.jarta.tests.NonFeasibleCorrelationException;
import de.unibw.jarta.tests.NotStationaryException;

public class SimpleModelExpDist {

	/**
	 * @param args
	 * @throws NonFeasibleCorrelationException 
	 * @throws NotStationaryException 
	 */
	public static void main(String[] args) throws NonFeasibleCorrelationException, NotStationaryException {
		
		RealDistribution distribution = new ExponentialDistribution(1.0);
		double[] artaCorrelationCoefficients =  {0.3, -0.1};
		ArtaProcess arta = ArtaProcessFactory.createArtaProcess(distribution, artaCorrelationCoefficients);
		
		System.out.println(arta);
		
		double[] data = new double[10000];
		for (int i = 0; i < data.length; i++) {
			data[i] = arta.next();
		}
		
		System.out.println("accs_arta	= " + Arrays.toString(AutoCorrelation.calculateAcfs(data, artaCorrelationCoefficients.length+2)));
		System.out.println("mean 		= " + StatUtils.mean(data));
		System.out.println("variance 	= " + StatUtils.variance(data));
		System.out.println("max 		= " + StatUtils.max(data));
		System.out.println("min 		= " + StatUtils.min(data));
		for (int d = 25; d < 100; d = d + 25 ) System.out.println(d+"%-percentil 	= " + StatUtils.percentile(data, d));
		
	}

}
