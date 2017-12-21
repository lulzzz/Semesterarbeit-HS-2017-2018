using Arta.Math;
using System;

namespace Arta.Distribution
{
    public static class DistributionFactory
    {
        public static IBaseDistribution CreateDistribution(ArtaExecutionContext.Distribution distribution)
        {
            switch (distribution)
            {
                case ArtaExecutionContext.Distribution.NormalDistribution:
                    return new NormalDistribution();
                case ArtaExecutionContext.Distribution.ContinousUniformDistribution:
                    return new ContinuousUniformDistribution();
                case ArtaExecutionContext.Distribution.ExponentialDistribution:
                    return new ExponentialDistribution();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
