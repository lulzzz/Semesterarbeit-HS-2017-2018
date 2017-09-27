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
import org.apache.commons.math3.distribution.RealDistribution;

public class ArtaProcessGeneral extends AbstractArtaProcess {
	private final RealDistribution distribution;
	private final NormalDistribution normal = new NormalDistribution();
	
	ArtaProcessGeneral(ArProcess ar, RealDistribution distribution) {
		super(ar);
		this.distribution = distribution;
		// TODO Auto-generated constructor stub
	}

	@Override
	protected double transform(double value) {
		// Transform to uniform distribution U(0,1)
		double result = normal.cumulativeProbability(value);
		// Transform to desired distribution
		result = distribution.inverseCumulativeProbability(result);
		return result;
	}
	public String toString(){
		StringBuilder result = new StringBuilder("ARTA process \n");
		result.append("Distribution = ").append(distribution.getClass().getSimpleName()).append("\n");
		result.append(getArProcess());
		return result.toString();
		
	}

}
