using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Math
{
    public abstract class DistributionState
    {
        public abstract void Handle(Context context);
        public double Rate { get; set; }
        public abstract double InverseCumulativeDistribution(double p);
        public abstract double GetLowerBound();
        public abstract double GetUpperBound();
        public abstract double GetMean();
        public abstract double GetVariance();
    }
}
