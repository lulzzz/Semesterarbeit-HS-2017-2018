using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    class FirstMoment : AbstractUnivariateStatistic
    {
        public long n;
        public double m1;
        public double dev;
        public double nDev;

        public FirstMoment()
        {
            this.n = 0L;
            this.m1 = Double.NaN;
            this.dev = Double.NaN;
            this.nDev = Double.NaN;
        }

        public FirstMoment(FirstMoment original)
        {
            Copy(original, this);
        }

        public void Increment(double d)
        {
            if (this.n == 0L)
            {
                this.m1 = 0.0D;
            }
            this.n += 1L;
            double n0 = this.n;
            this.dev = (d - this.m1);
            this.nDev = (this.dev / n0);
            this.m1 += this.nDev;
        }

        public void Clear()
        {
            this.m1 = Double.NaN;
            this.n = 0L;
            this.dev = Double.NaN;
            this.nDev = Double.NaN;
        }

        public double GetResult()
        {
            return this.m1;
        }

        public long GetN()
        {
            return this.n;
        }

        public FirstMoment Copy()
        {
            FirstMoment result = new FirstMoment();

            Copy(this, result);
            return result;
        }

        public static void Copy(FirstMoment source, FirstMoment dest)
        {
            MathUtils.checkNotNull(source);
            MathUtils.checkNotNull(dest);
            dest.SetData(source.GetDataRef());
            dest.n = source.n;
            dest.m1 = source.m1;
            dest.dev = source.dev;
            dest.nDev = source.nDev;
        }
    }
}
