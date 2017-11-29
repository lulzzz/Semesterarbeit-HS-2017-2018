using Arta.Math;
using MathNet.Numerics.Distributions;

namespace Arta
{
    class ArtaProcessGeneral : AbstractArtaProcess
    {
        private DistributionState distribution;
        private readonly Normal normal = new Normal();
        public ArtaProcessGeneral(ArProcess ar, DistributionState distribution) : base(ar)
        {
            this.distribution = distribution;
        }
        protected override double Transform(double value)
        {
            var result = normal.CumulativeDistribution(value);
            
            result = distribution.InverseCumulativeDistribution(result);
            return result;
        }
    }
}
