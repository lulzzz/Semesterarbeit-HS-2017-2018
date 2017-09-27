package de.unibw.jarta.examples;

import java.util.Arrays;

import org.apache.commons.math3.distribution.ExponentialDistribution;
import org.apache.commons.math3.distribution.RealDistribution;

import de.unibw.jarta.arta.ArtaProcess;
import de.unibw.jarta.arta.ArtaProcessFactory;
import de.unibw.jarta.fitting.OrderEstimator;
import de.unibw.jarta.math.AutoCorrelation;
import de.unibw.jarta.tests.NonFeasibleCorrelationException;
import de.unibw.jarta.tests.NotStationaryException;

public class TestPacf {

	/**
	 * @param args
	 * @throws NotStationaryException 
	 * @throws NonFeasibleCorrelationException 
	 */
	public static void main(String[] args) throws NonFeasibleCorrelationException, NotStationaryException {
		
		
		RealDistribution distribution = new ExponentialDistribution(1.0);
		double[] artaCorrelationCoefficients =  {0.3, 0.3, -0.1};
		ArtaProcess arta = ArtaProcessFactory.createArtaProcess(distribution, artaCorrelationCoefficients);
		
		double[] data = new double[10000];
		for (int i = 0; i < data.length; i++) {
			data[i] = arta.next();
		}
		
		
		int maxLag 		= 10;
		double[] acfs 	= AutoCorrelation.calculateAcfs(data, maxLag);
		double[] pacfs 	= AutoCorrelation.calculatePacfs(acfs);
		
		System.out.println(Arrays.toString(acfs));

		System.out.println(Arrays.toString(pacfs));
		
		System.out.println("Order = " + OrderEstimator.estimateOrder(data, maxLag));

	}

}
