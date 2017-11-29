﻿using Arta.Math;
using MathNet.Numerics.Distributions;

namespace Arta
{
    class ArtaProcessGeneral : AbstractArtaProcess
    {
        private Math.IDistribution distribution;
        private readonly Normal normal = new Normal();
        public ArtaProcessGeneral(ArProcess ar, Math.IDistribution distribution) : base(ar)
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
