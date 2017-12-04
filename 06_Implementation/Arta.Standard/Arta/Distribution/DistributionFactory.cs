using Arta.Math;
using System;

namespace Arta
{
    public static class DistributionFactory
    {
        public static BaseDistribution CreateDistribution(BaseDistribution.Distribution distribution)
        {
            switch (distribution)
            {
                case BaseDistribution.Distribution.NormalDistribution:
                    return new NormalDistribution();
                case BaseDistribution.Distribution.ContinousUniformDistribution:
                    return new ContinuousUniformDistribution();
                case BaseDistribution.Distribution.ExponentialDistribution:
                    return new ExponentialDistribution();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
