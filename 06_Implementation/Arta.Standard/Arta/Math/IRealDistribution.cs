using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Math
{
    interface IRealDistribution : IContinuousDistribution
    {
        double Probability(double paramDouble);
        double CumulativeProbability(double paramDouble);
        double CumulativeProbability(double paramDouble1, double paramDouble2); // throws NumberIsTooLargeException;
        double InverseCumulativeProbability(double paramDouble); //throws OutOfRangeException;
        double GetSupportLowerBound();
        double GetSupportUpperBound();
        bool IsSupportLowerBoundInclusive();
        bool IsSupportUpperBoundInclusive();
        bool IsSupportConnected();
    }
}
