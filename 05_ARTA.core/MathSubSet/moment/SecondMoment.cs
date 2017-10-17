using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    class SecondMoment : FirstMoment
    {
        public double m2;

        public SecondMoment()
        {
            this.m2 = Double.NaN;
        }

        public SecondMoment(SecondMoment original) : base(original)
        {
            this.m2 = original.m2;
        }

        public new void Increment(double d)
        {
            if (this.n < 1L)
            {
                this.m1 = (this.m2 = 0.0D);
            }
            base.Increment(d);
            this.m2 += (this.n - 1.0D) * this.dev * this.nDev;
        }

        public new void Clear()
        {
            base.Clear();
            this.m2 = Double.NaN;
        }

        public new double GetResult()
        {
            return this.m2;
        }

        public new SecondMoment Copy()
        {
            SecondMoment result = new SecondMoment();

            Copy(this, result);
            return result;
        }

        public static void Copy(SecondMoment source, SecondMoment dest)
        {
            MathUtils.checkNotNull(source);
            MathUtils.checkNotNull(dest);
            FirstMoment.Copy(source, dest);
            dest.m2 = source.m2;
        }
    }
}
