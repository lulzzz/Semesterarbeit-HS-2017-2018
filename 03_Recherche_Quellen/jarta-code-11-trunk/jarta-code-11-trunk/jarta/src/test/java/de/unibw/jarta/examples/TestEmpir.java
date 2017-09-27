package de.unibw.jarta.examples;

import org.apache.commons.math3.distribution.RealDistribution;

import de.unibw.jarta.math.EmpiricalDistribution;

public class TestEmpir {

	/**
	 * @param args
	 */
	public static void main(String[] args) { 
		double[] values = {1,2,3,2};
		RealDistribution dist = new EmpiricalDistribution(values);
		for (double i = 0; i <= 10; i++ ){
			System.out.println(dist.inverseCumulativeProbability(i/10));
		}
		

	}

}
