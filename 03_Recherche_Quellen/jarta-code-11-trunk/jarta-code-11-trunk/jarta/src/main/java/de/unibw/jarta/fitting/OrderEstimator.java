package de.unibw.jarta.fitting;

import de.unibw.jarta.math.AutoCorrelation;

public class OrderEstimator {

	public final static int DEFAULT_MAX_ORDER = 100;
	
	public static int estimateOrder(double[] data, int maxOrder) {
		double significanceLevel = 2/Math.sqrt(data.length);
		int order = maxOrder;
		double[] acfs =  AutoCorrelation.calculateAcfs(data, maxOrder);
		double[] pacfs = AutoCorrelation.calculatePacfs(acfs);
		
		for (int i = 0; i < maxOrder; i++) {
			if (Math.abs(pacfs[i+1]) < significanceLevel) {
				return i;
			}
			
		}
		
		return order;
		
	}

	public static int estimateOrder(double[] data) {
		int maxOrder = Math. min(data.length / 4, DEFAULT_MAX_ORDER);
		return estimateOrder(data, maxOrder);
	}

}
