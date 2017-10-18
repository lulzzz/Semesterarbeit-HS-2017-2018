using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.ArtaExtensions
{
    interface IRealDistribution
    {
        double Probability(double paramDouble);

        double Density(double paramDouble);

        double CumulativeProbability(double paramDouble);

        [Obsolete]
        double CumulativeProbability(double paramDouble1, double paramDouble2); // throws NumberIsTooLargeException;

        double InverseCumulativeProbability(double paramDouble); //throws OutOfRangeException;

        double GetNumericalMean();

        double GetNumericalVariance();

        double GetSupportLowerBound();

        double GetSupportUpperBound();

        [Obsolete]
        bool IsSupportLowerBoundInclusive();

        [Obsolete]
        bool IsSupportUpperBoundInclusive();

        bool IsSupportConnected();

        void ReseedRandomGenerator(long paramLong);

        double Sample();

        double[] Sample(int paramInt);
    }
}
