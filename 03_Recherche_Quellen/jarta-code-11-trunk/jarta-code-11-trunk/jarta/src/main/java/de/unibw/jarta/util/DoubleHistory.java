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
package de.unibw.jarta.util;

import de.unibw.jarta.util.ValueHistory;

public class DoubleHistory implements ValueHistory<Double> {
	private final int size;
	private final double[] values;
	private int addIndex;



	public DoubleHistory(int size) {
		this.size = size;
		this.values = new double[size];
		for (int i = 0; i < size; i++) {
			values[i] = 0.0;
		}
		this.addIndex = 0;
	}



	@Override
	public void add(Double value) {
		values[addIndex] = value;
		addIndex++;
		if (addIndex >= size) {
			addIndex = 0;
		}
	}



	@Override
	public Double get(int index) {
		int i = addIndex + size - index - 1;
		if (i >= size) {
			i = i - size;
		}
		return values[i];
	}



	@Override
	public int size() {
		return size;
	}



	public static void main(String[] args) {
		DoubleHistory test = new DoubleHistory(3);
		test.add(1.0);
		test.add(2.0);
		test.add(3.0);
		test.add(4.0);
		for (int i = 0; i < test.size(); i++) {
			System.out.print(test.get(i) + "\t");
		}
	}
}
