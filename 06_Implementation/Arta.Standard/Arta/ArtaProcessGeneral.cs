using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    class ArtaProcessGeneral : AbstractArtaProcess
    {
        private ContinuousUniform dist;
        private readonly Normal normal = new Normal();
        public ArtaProcessGeneral(ArProcess ar, ContinuousUniform dist) : base(ar)
        {
            this.dist = dist;
        }
        protected override double Transform(double value)
        {
            double result = normal.CumulativeDistribution(value);
            result = dist.InverseCumulativeDistribution(result);
            return result;
        }
    }
}
