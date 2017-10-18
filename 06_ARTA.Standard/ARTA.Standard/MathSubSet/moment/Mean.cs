using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    class Mean : AbstractStorelessUnivariateStatistic
    {
        protected FirstMoment moment;
        protected bool incMoment;

        public Mean()
        {
            this.incMoment = true;
            this.moment = new FirstMoment();
        }

        public Mean(FirstMoment m1)
        {
            this.moment = m1;
            this.incMoment = false;
        }

        public Mean(Mean original)
        {
            Copy(original, this);
        }

        public new void Increment(double d)
        {
            if (this.incMoment)
            {
                this.moment.Increment(d);
            }
        }

        public new void Clear()
        {
            if (this.incMoment)
            {
                this.moment.Clear();
            }
        }

        public double GetResult()
        {
            return this.moment.m1;
        }

        public long GetN()
        {
            return this.moment.GetN();
        }

        public double Evaluate(double[] values, int begin, int length)
        {
            if (Test(values, begin, length))
            {
                Sum sum = new Sum();
                double sampleSize = length;

                double xbar = sum.Evaluate(values, begin, length) / sampleSize;

                double correction = 0.0D;
                for (int i = begin; i < begin + length; i++)
                {
                    correction += values[i] - xbar;
                }
                return xbar + correction / sampleSize;
            }
            return Double.NaN;
        }

        public double Evaluate(double[] values, double[] weights, int begin, int length)
        {
            if (Test(values, weights, begin, length))
            {
                Sum sum = new Sum();

                double sumw = sum.Evaluate(weights, begin, length);
                double xbarw = sum.Evaluate(values, weights, begin, length) / sumw;

                double correction = 0.0D;
                for (int i = begin; i < begin + length; i++)
                {
                    correction += weights[i] * (values[i] - xbarw);
                }
                return xbarw + correction / sumw;
            }
            return Double.NaN;
        }

        public double Evaluate(double[] values, double[] weights)
        {
            return Evaluate(values, weights, 0, values.Length);
        }

        public Mean Copy()
        {
            Mean result = new Mean();

            Copy(this, result);
            return result;
        }

        public static void Copy(Mean source, Mean dest)
        {
            MathUtils.checkNotNull(source);
            MathUtils.checkNotNull(dest);
            dest.SetData(source.GetDataRef());
            dest.incMoment = source.incMoment;
            dest.moment = source.moment.Copy();
        }
    }
}
