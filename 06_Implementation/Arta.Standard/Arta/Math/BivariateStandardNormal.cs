namespace Arta.Math
{
    static class BivariateStandardNormal
    {
        public static double GetDensity(double x, double y, double rho)
        {
            var d = 1.0 - rho * rho;
            return System.Math.Exp(-(x * x - 2.0 * rho * x * y + y * y) / (2 * d)) / (2 * System.Math.PI * System.Math.Sqrt(d));
        }
    }
}
