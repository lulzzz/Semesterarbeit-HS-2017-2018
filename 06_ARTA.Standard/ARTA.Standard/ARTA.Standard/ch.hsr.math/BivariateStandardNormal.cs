using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.math
{
    class BivariateStandardNormal
    {
        private BivariateStandardNormal()
        {
            // no instantiation
        }
        public static double GetDensity(double x, double y, double rho)
        {
            double d = 1.0 - rho * rho;
            return Math.Exp(-(x * x - 2.0 * rho * x * y + y * y) / (2 * d)) / (2 * Math.PI * Math.Sqrt(d));
        }
    }
}
