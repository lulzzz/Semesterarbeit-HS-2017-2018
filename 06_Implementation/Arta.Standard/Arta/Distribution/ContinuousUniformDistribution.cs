using MathNet.Numerics.Distributions;

namespace Arta.Math
{
    public class ContinuousUniformDistribution : DistributionState
    {
        ContinuousUniform continuousUniform;

        public override void Handle(Context context)
        {
            continuousUniform = new ContinuousUniform();
        }

        public override double InverseCumulativeDistribution(double p)
        {
            return continuousUniform.InverseCumulativeDistribution(p);
        }
        public override double GetLowerBound()
        {
            return continuousUniform.LowerBound;
        }

        public override double GetMean()
        {
            return continuousUniform.Mean;
        }

        public override double GetUpperBound()
        {
            return continuousUniform.UpperBound;
        }

        public override double GetVariance()
        {
            return continuousUniform.Variance;
        }

     
    }
}
