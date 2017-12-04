namespace Arta.Tests
{
    public static class StationaryTest
    {
        public static bool IsStationary(double[] alphas)
        {
            var dim = alphas.Length;
            var a = new double[dim + 1];
            var b = new double[dim + 1];

            a[0] = -1.0;
            for(var i = 0; i < dim; i++)
            {
                a[i + 1] = alphas[i];
            }

            for(var k = dim; k > 0; k--)
            {
                for(var j = 0; j < k; j++)
                {
                    b[j] = a[0] * a[j] - a[k] * a[k - j];
                }
                if(b[0] <= 0.0)
                {
                    //process is not stationary
                    return false;
                }

                for(var i = 0; i < dim + 1; i++)
                {
                    a[i] = b[i];
                }
            }
            //process is stationary
            return true;
        }
    }
}
