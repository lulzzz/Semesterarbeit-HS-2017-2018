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

public class ArtaProcessNormal extends AbstractArtaProcess {
	private final double stdev;
	private final double mean;

	ArtaProcessNormal(ArProcess ar, double mean, double variance) {
		super(ar);
		this.mean = mean;
		this.stdev = Math.sqrt(variance);
	}

	@Override
	protected double transform(double value) {
		return value * stdev + mean;
	}

}
