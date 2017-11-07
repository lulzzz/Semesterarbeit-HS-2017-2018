using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    class Sum : AbstractUnivariateStatistic
    {
        private long n;
        private double value;

        public Sum()
        {
            this.n = 0L;
            this.value = 0.0D;
        }

        public Sum(Sum original)
        {
            Copy(original, this);
        }

        public void Increment(double d)
        {
            this.value += d;
            this.n += 1L;
        }

        public double GetResult()
        {
            return this.value;
        }

        public long GetN()
        {
            return this.n;
        }

        public void Clear()
        {
            this.value = 0.0D;
            this.n = 0L;
        }

        public double Evaluate(double[] values, int begin, int length)
        {
            double sum = Double.NaN;
            if (Test(values, begin, length, true))
            {
                sum = 0.0D;
                for (int i = begin; i < begin + length; i++)
                {
                    sum += values[i];
                }
            }
            return sum;
        }

        public double Evaluate(double[] values, double[] weights, int begin, int length)
        {
            double sum = Double.NaN;
            if (Test(values, weights, begin, length, true))
            {
                sum = 0.0D;
                for (int i = begin; i < begin + length; i++)
                {
                    sum += values[i] * weights[i];
                }
            }
            return sum;
        }

        public double Evaluate(double[] values, double[] weights)
        {
            return Evaluate(values, weights, 0, values.Length);
        }

        public Sum Copy()
        {
            Sum result = new Sum();

            Copy(this, result);
            return result;
        }

        public static void Copy(Sum source, Sum dest)
        {
            MathUtils.checkNotNull(source);
            MathUtils.checkNotNull(dest);
            dest.SetData(source.GetDataRef());
            dest.n = source.n;
            dest.value = source.value;
        }
    }
}
