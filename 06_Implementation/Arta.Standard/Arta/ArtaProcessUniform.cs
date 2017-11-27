using MathNet.Numerics.Distributions;

namespace Arta
{
    class ArtaProcessUniform : AbstractArtaProcess
    {
        private readonly Normal normal = new Normal();
        private readonly double lower, difference;

        public ArtaProcessUniform(ArProcess ar, double lower, double upper) : base(ar)
        {
            this.lower = lower;
            difference = upper - lower;
        }
        protected override double Transform(double value)
        {
            var result = normal.CumulativeDistribution(value);
            result = result * difference + lower;
            return result;
        }
    }
}
