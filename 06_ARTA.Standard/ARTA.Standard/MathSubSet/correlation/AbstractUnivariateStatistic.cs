using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    abstract class AbstractUnivariateStatistic
    {
        private double[] storedData;

        public void SetData(double[] values)
        {
            this.storedData = (values == null ? null : (double[])values.Clone());
        }

        public double[] GetData()
        {
            return this.storedData == null ? null : (double[])this.storedData.Clone();
        }

        public double[] GetDataRef()
        {
            return this.storedData;
        }

        protected bool Test(double[] values, int begin, int length)
        {
            return MathArrays.verifyValues(values, begin, length, false);
        }

        protected bool Test(double[] values, double[] weights, int begin, int length)
        {
            return MathArrays.verifyValues(values, weights, begin, length, false);
        }

        protected bool Test(double[] values, int begin, int length, bool allowEmpty)
        {
            return MathArrays.verifyValues(values, begin, length, allowEmpty);
        }

        protected bool Test(double[] values, double[] weights, int begin, int length, bool allowEmpty)
        {
            return MathArrays.verifyValues(values, weights, begin, length, allowEmpty);
        }
    }
}
