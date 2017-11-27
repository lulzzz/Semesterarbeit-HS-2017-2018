using MathNet.Numerics.Distributions;
using System;

namespace Arta.Math
{
    public class ExponentialDistribution : DistributionState
    {
        Exponential exponential;

        public override void Handle(Context context)
        {
            exponential = new Exponential(Rate);
        }
        public override double GetLowerBound()
        {
            throw new NotImplementedException();
        }

        public override double GetMean()
        {
            return exponential.Mean;
        }

        public override double GetUpperBound()
        {
            throw new NotImplementedException();
        }

        public override double GetVariance()
        {
            return exponential.Variance;
        }


        public override double InverseCumulativeDistribution(double p)
        {
            return exponential.InverseCumulativeDistribution(p);
        }
    }
}
