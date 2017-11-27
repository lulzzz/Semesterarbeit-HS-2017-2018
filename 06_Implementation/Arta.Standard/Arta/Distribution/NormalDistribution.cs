using MathNet.Numerics.Distributions;

namespace Arta.Math
{
    public class NormalDistribution : DistributionState
    {
        Normal normal;

      
        public override void Handle(Context context)
        {            
            normal = new Normal();
        }

        public override double InverseCumulativeDistribution(double p)
        {
            return normal.InverseCumulativeDistribution(p);
        }

        public override double GetLowerBound()
        {
            throw new System.NotImplementedException();
        }

        public override double GetMean()
        {
            return normal.Mean;
        }

        public override double GetUpperBound()
        {
            throw new System.NotImplementedException();
        }

        public override double GetVariance()
        {
            return normal.Variance;
        }

  
    }
}
