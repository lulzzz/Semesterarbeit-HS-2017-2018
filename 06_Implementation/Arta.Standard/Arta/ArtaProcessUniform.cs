using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    class ArtaProcessUniform : AbstractArtaProcess
    {
        private readonly Normal normal = new Normal();
        private readonly double lower, difference;

        public ArtaProcessUniform(ArProcess ar, double lower, double upper) : base(ar)
        {
            this.lower = lower;
            this.difference = upper - lower;
        }
        protected override double Transform(double value)
        {
            var result = normal.CumulativeDistribution(value);
            result = result * difference + lower;
            return result;
        }
    }
}
