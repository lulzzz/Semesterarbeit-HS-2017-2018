using Math3.analysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.ArtaExtensions
{
    interface IUnivariateIntegrator
    {
        double getRelativeAccuracy();

        double getAbsoluteAccuracy();

        int getMinimalIterationCount();

        int getMaximalIterationCount();

        double integrate(int paramInt, UnivariateFunction paramUnivariateFunction, double paramDouble1, double paramDouble2); //throws TooManyEvaluationsException, MaxCountExceededException, MathIllegalArgumentException, NullArgumentException;

        int getEvaluations();

        int getIterations();
    }
}
