using Math3.exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    interface IStorelessUnivariateStatistic
    {
        void Increment(double paramDouble);

        void IncrementAll(double[] paramArrayOfDouble); //throw MathIllegalArgumentException;

        void IncrementAll(double[] paramArrayOfDouble, int paramInt1, int paramInt2); //throw MathIllegalArgumentException;

        double GetResult();

        long GetN();

        void Clear();

         IStorelessUnivariateStatistic Copy();
    }
}
