using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    class ArtaProcessNormal : AbstractArtaProcess
    {
        private readonly double stdev, mean;
        public ArtaProcessNormal(ArProcess ar, double mean, double variance) : base(ar)
        {
            this.mean = mean;
            this.stdev = Math.Sqrt(variance);
        }

        protected override double Transform(double value)
        {
            return value * stdev + mean;
        }
    }
}
