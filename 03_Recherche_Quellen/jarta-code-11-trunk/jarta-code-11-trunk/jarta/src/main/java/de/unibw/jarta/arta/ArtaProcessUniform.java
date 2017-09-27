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

public class ArtaProcessUniform extends AbstractArtaProcess {
	private final NormalDistribution normal = new NormalDistribution();
	private final double lower;
	private final double difference;
	

	ArtaProcessUniform(ArProcess ar, double lower, double upper) {
		super(ar);
		this.lower = lower;
		this.difference = upper -lower;
	}
	
	protected double transform(double value) {
		// Transform to uniform distribution U(0,1)
		double result = normal.cumulativeProbability(value);
		// Transform to desired range
		result = result * difference + lower;
		return result;		
	}

}
