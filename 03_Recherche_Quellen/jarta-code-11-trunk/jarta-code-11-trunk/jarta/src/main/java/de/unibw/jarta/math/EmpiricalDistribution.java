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

import java.util.Arrays;

import org.apache.commons.math3.distribution.AbstractRealDistribution;
import org.apache.commons.math3.exception.OutOfRangeException;
import org.apache.commons.math3.random.Well1024a;
import org.apache.commons.math3.stat.StatUtils;



public class EmpiricalDistribution extends AbstractRealDistribution {
	private static final long	serialVersionUID	= 1L;
	private final int			nPoints;
	private final double[]		points;
	private final double		mean;
	private final double		variance;
	private final double		lowerBound;
	private final double		upperBound;



	public EmpiricalDistribution(double[] values) {
		super(new Well1024a());
		this.nPoints = values.length;
		points = Arrays.copyOf(values, nPoints);
		Arrays.sort(points);
		this.lowerBound = points[0];
		this.upperBound = points[nPoints-1];
		this.mean = StatUtils.mean(values);
		// populationVariance vs. Variance
		this.variance = StatUtils.variance(values);

	}



	@Override
	public double getNumericalMean() {
		return mean;
	}



	@Override
	public double getNumericalVariance() {
		return variance;
	}



	@Override
	public double density(double x) {
		
		// binary search lower and upper bound
		// return up_index - low_index / size
		// TODO Auto-generated method stub
		return 0;
	}



	@Override
	public double cumulativeProbability(double x) {
		// binary search upper bound
		// return index/size
		
		// TODO Auto-generated method stub
		return 0;
	}
	
	@Override
	public double inverseCumulativeProbability(final double p) throws OutOfRangeException {
		//handle special cases and errors
		if (p <= 0.0 || p >= 1.0) {
			if (p == 1.0) {
				return upperBound;
			}
			if (p == 0.0) {
				return lowerBound;
			}	
			throw new OutOfRangeException(p, 0, 1);
		}
		int index = (int) (p * nPoints);
		return points[index];
	}


	@Override
	public double getSupportLowerBound() {
		return lowerBound;
	}



	@Override
	public double getSupportUpperBound() {
		return upperBound;
	}



	@Override
	public boolean isSupportLowerBoundInclusive() {
		return true;
	}



	@Override
	public boolean isSupportUpperBoundInclusive() {
		return true;
	}



	@Override
	public boolean isSupportConnected() {
		return true;
	}

}
