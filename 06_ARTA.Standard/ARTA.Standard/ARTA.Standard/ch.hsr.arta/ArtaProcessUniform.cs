using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    class ArtaProcessUniform : AbstractArtaProcess
    {
        private readonly NormalDistribution normal = new NormalDistribution();
        private readonly double lower, difference;
        public ArtaProcessUniform(ArProcess ar, double lower, double difference) : base(ar)
        {
            this.lower = lower;
            this.difference = difference;
        }

        protected override double Transform(double value)
        {
            double result = normal.CumulativeProbability(value);
            result = result * difference + lower;
            return result;
        }
    }
}
