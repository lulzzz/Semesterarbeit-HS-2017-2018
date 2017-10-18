using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    abstract class AbstractStorelessUnivariateStatistic : AbstractUnivariateStatistic, IStorelessUnivariateStatistic
    {

        public double Evaluate(double[] values)
        {
            if (values == null)
            {
                // throw new NullArgumentException(LocalizedFormats.INPUT_ARRAY, new Object[0]);
            }
            return Evaluate(values, 0, values.Length);
        }

        public double Evaluate(double[] values, int begin, int length)//throws MathIllegalArgumentException
        {
            if (Test(values, begin, length))
            {
                Clear();
                IncrementAll(values, begin, length);
            }
            return GetResult();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IStorelessUnivariateStatistic Copy()
        {
            throw new NotImplementedException();
        }

        public long GetN()
        {
            throw new NotImplementedException();
        }

        public double GetResult()
        {
            throw new NotImplementedException();
        }

        public void Increment(double paramDouble)
        {
            throw new NotImplementedException();
        }

        public void IncrementAll(double[] paramArrayOfDouble)
        {
            throw new NotImplementedException();
        }

        public void IncrementAll(double[] paramArrayOfDouble, int paramInt1, int paramInt2)
        {
            throw new NotImplementedException();
        }
    }
}
