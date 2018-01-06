using Arta.Distribution;
using MathNet.Numerics.Distributions;

namespace Arta
{
    public class ArtaProcessGeneral : AbstractArtaProcess
    {
        private IBaseDistribution distribution;
        private readonly Normal normal = new Normal();
        public ArtaProcessGeneral(ArProcess ar, IBaseDistribution distribution) : base(ar)
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
