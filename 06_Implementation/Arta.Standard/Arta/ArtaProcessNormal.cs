using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    class ArtaProcessNormal : AbstractArtaProcess
    {
        public readonly double stdev, mean;

        public ArtaProcessNormal(ArProcess ar, double mean, double variance) : base(ar)
        {
            this.mean = mean;
            stdev = System.Math.Sqrt(variance);
        }

        protected override double Transform(double value)
        {
            return value * stdev + mean;
        }
    }
}
