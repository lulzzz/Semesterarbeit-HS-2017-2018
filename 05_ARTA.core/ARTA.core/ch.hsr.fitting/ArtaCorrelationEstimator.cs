using ARTA.core.ch.hsr.math;
using ARTA.core.ch.hsr.util;
using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.fitting
{
    class ArtaCorrelationEstimator
    {
        /** Abscissae used for the Gauss and Kronrod weights in interval -1.0 to 1.0 */
        private readonly double[] abscissae =  { 0.991455371120812639206854697526329,
                                                0.949107912342758524526189684047851,
                                                0.864864423359769072789712788640926,
                                                0.741531185599394439863864773280788,
                                                0.586087235467691130294144838258730,
                                                0.405845151377397166906606412076961,
                                                0.207784955007898467600689403773245,
                                                0.0,
                                               -0.207784955007898467600689403773245,
                                               -0.405845151377397166906606412076961,
                                               -0.586087235467691130294144838258730,
                                               -0.741531185599394439863864773280788,
                                               -0.864864423359769072789712788640926,
                                               -0.949107912342758524526189684047851,
                                               -0.991455371120812639206854697526329};

        /** The Kronrod weights corresponding to the defined abscissae */
        private double[] kronrodWeights = { 0.022935322010529224963732008058970,
                                            0.063092092629978553290700663189204,
                                            0.104790010322250183839876322541518,
                                            0.140653259715525918745189590510238,
                                            0.169004726639267902826583426598550,
                                            0.190350578064785409913256402421014,
                                            0.204432940075298892414161999234649,
                                            0.209482141084727828012999174891714,
                                            0.204432940075298892414161999234649,
                                            0.190350578064785409913256402421014,
                                            0.169004726639267902826583426598550,
                                            0.140653259715525918745189590510238,
                                            0.104790010322250183839876322541518,
                                            0.063092092629978553290700663189204,
                                            0.022935322010529224963732008058970};

        /** The Gauss weights corresponding to the defined abscissae */
        private readonly double[] gaussWeights = { 0.0,
                                                   0.129484966168869693270611432679082,
                                                   0.0,
                                                   0.279705391489276667901467771423780,
                                                   0.0,
                                                   0.381830050505118944950369775488975,
                                                   0.0,
                                                   0.417959183673469387755102040816327,
                                                   0.0,
                                                   0.381830050505118944950369775488975,
                                                   0.0,
                                                   0.279705391489276667901467771423780,
                                                   0.0,
                                                   0.129484966168869693270611432679082,
                                                   0.0};

        private RealDistribution distribution;
	    private NormalDistribution standardNormal = new NormalDistribution();
        private LruCache<Double, Double> estimationsCache = new LruCache<Double, Double>(100);
        private LruCache<Double, Double> transformationCache = new LruCache<Double, Double>(1000);

        private static double TOLERANCE_OUTER = 0.00001;
        private static double TOLERANCE_INNER = 0.00005;

        public ArtaCorrelationEstimator(RealDistribution dist)
        {
            this.distribution = dist;
        }
        public double estimateArtaCorrelation(double arAutocorrelation)
        {
            Double result = estimationsCache.get(arAutocorrelation);
            if(result == null)
            {
                double e = Integrate(-8, 8, arAutocorrelation);
                double mean = distribution.getNumericalMean();
                double variance = distribution.getNumericalVariance();
                result = (e - mean * mean) / variance;
                estimationsCache.put(arAutocorrelation, result);
            }
            return result;
        }

        private double Integrate(double from, double to, double rho)
        {
            double kronrodSum = 0.0;
            double gaussSum = 0.0;
            double x = 0.0;
            double xt = 0.0;
            double center = (from + to) / 2.0;
            double halfDistance = to - center;

            for (int i = 0; i < abscissae.Length; i++)
            {
                // linear mapping
                x = abscissae[i] * halfDistance + center;
                // transformation
                xt = transform(x);
                kronrodSum += xt * IntegrateInner(-8, 8, rho, x) * kronrodWeights[i];
                gaussSum += xt * IntegrateInner(-8, 8, rho, x) * gaussWeights[i];
            }

            kronrodSum = halfDistance * kronrodSum;
            gaussSum = halfDistance * gaussSum;

            if (Math.Abs(kronrodSum - gaussSum) > TOLERANCE_OUTER)
            {
                kronrodSum = Integrate(from, center, rho) + Integrate(center, to, rho);
            }
            return kronrodSum;
        }



        private double IntegrateInner(double from, double to, double rho, double x)
        {
            double kronrodSum = 0.0;
            double gaussSum = 0.0;
            double y = 0.0;
            double yt = 0.0;
            double densityBN = 0.0;
            double center = (from + to) / 2.0;
            double halfDistance = to - center;

            for (int i = 0; i < abscissae.Length; i++)
            {
                y = abscissae[i] * halfDistance + center;
                yt = transform(y);
                densityBN = BivariateStandardNormal.GetDensity(x, y, rho);
                kronrodSum += yt * densityBN * kronrodWeights[i];
                gaussSum += yt * densityBN * gaussWeights[i];
            }

            kronrodSum = halfDistance * kronrodSum;
            gaussSum = halfDistance * gaussSum;

            if (Math.Abs(kronrodSum - gaussSum) > TOLERANCE_INNER)
            {
                kronrodSum = IntegrateInner(from, center, rho, x) + IntegrateInner(center, to, rho, x);
            }
            return kronrodSum;
        }

        private double transform(double value)
        {
            Double result = transformationCache.Get(value);
            if (result == null)
            {
                // Transform to uniform distribution U(0,1)
                result = standardNormal.CumulativeProbability(value);
                // Transform to desired distribution
                result = distribution.inverseCumulativeProbability(result);
                transformationCache.Put(value, result);
            }
            return result;
        }
    }
}
