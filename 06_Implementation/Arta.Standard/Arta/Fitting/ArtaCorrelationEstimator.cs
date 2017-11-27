using Arta.Math;
using Arta.Util;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Fitting
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
        private readonly double[] kronrodWeights = { 0.022935322010529224963732008058970,
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

        private readonly Normal standardNormal = new Normal();
        private readonly DistributionState distribution;

        private const double Tolerance_Outer = 0.00001;
        private const double Tolerance_Inner = 0.00005;

        private LruCache<double, double> estimationsCache = new LruCache<double, double>(100);
        private LruCache<double, double> transformationCache = new LruCache<double, double>(1000);

        public ArtaCorrelationEstimator(DistributionState distribution)
        {
            this.distribution = distribution;
        }

        public double EstimateArtaCorrelation(double arAutocorrelation)
        {
            double? result = estimationsCache.Get(arAutocorrelation);
            if (result == 0)
            {
                double e = Integrate(-8, 8, arAutocorrelation);
                double mean = distribution.GetMean();
                double variance = distribution.GetVariance();
                result = (e - mean * mean) / variance;
                estimationsCache.Add(arAutocorrelation, (double)result);
            }
            return (double)result;
        }

        private double Integrate(double from, double to, double rho)
        {
            var kronrodSum = 0.0;
            var gaussSum = 0.0;
            var x = 0.0;
            var xt = 0.0;
            var center = (from + to) / 2.0;
            var halfDistance = to - center;

            for (var i = 0; i < abscissae.Length; i++)
            {
                x = abscissae[i] * halfDistance + center;
                xt = Transform(x);
                kronrodSum += xt * IntegrateInner(-8, 8, rho, x) * kronrodWeights[i];
                gaussSum += xt * IntegrateInner(-8, 8, rho, x) * gaussWeights[i];
            }

            kronrodSum = halfDistance * kronrodSum;
            gaussSum = halfDistance * gaussSum;

            if (System.Math.Abs(kronrodSum - gaussSum) > Tolerance_Outer)
            {
                kronrodSum = Integrate(from, center, rho) + Integrate(center, to, rho);
            }
            return kronrodSum;
        }

        private double IntegrateInner(double from, double to, double rho, double x)
        {
            var kronrodSum = 0.0;
            var gaussSum = 0.0;
            var y = 0.0;
            var yt = 0.0;
            var densityBN = 0.0;
            var center = (from + to) / 2.0;
            var halfDistance = to - center;

            for (var i = 0; i < abscissae.Length; i++)
            {
                y = abscissae[i] * halfDistance + center;
                yt = Transform(y);
                densityBN = BivariateStandardNormal.GetDensity(x, y, rho);
                kronrodSum += yt * densityBN * kronrodWeights[i];
                gaussSum += yt * densityBN * gaussWeights[i];
            }

            kronrodSum = halfDistance * kronrodSum;
            gaussSum = halfDistance * gaussSum;

            if (System.Math.Abs(kronrodSum - gaussSum) > Tolerance_Inner)
            {
                kronrodSum = IntegrateInner(from, center, rho, x) + IntegrateInner(center, to, rho, x);
            }
            return kronrodSum;
        }

        private double Transform(double value)
        {
        
            double? result = transformationCache.Get(value);
            if (result == 0)
            {
                result = standardNormal.CumulativeDistribution(value);
                result = distribution.InverseCumulativeDistribution((double)result);
                transformationCache.Add(value, (double)result);
            }
            return (double)result;
        }
    }
}
