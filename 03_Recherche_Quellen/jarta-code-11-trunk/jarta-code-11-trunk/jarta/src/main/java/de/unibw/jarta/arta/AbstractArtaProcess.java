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

abstract class AbstractArtaProcess implements ArtaProcess {
	/** The underlying autoregressive process */
	private final ArProcess	ar;



	/**
	 * Generates an ARTA process with a given underlying autoregressive process.
	 * 
	 * @param ar
	 *            the underlying autoregressive process
	 */
	AbstractArtaProcess(ArProcess ar) {
		this.ar = ar;
	}



	@Override
	public double next() {
		return transform(ar.next());
	}



	abstract protected double transform(double value);



	@Override
	public ArProcess getArProcess() {
		return ar;
	}

}
