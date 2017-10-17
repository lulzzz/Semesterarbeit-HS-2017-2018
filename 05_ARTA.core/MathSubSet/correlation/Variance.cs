using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.correlation
{
    class Variance :AbstractUnivariateStatistic
    {
        protected SecondMoment moment = null;
        protected bool incMoment = true;
        private bool isBiasCorrected = true;

        public Variance()
        {
            this.moment = new SecondMoment();
        }

        public Variance(SecondMoment m2)
        {
            this.incMoment = false;
            this.moment = m2;
        }

        public Variance(bool isBiasCorrected)
        {
            this.moment = new SecondMoment();
            this.isBiasCorrected = isBiasCorrected;
        }

        public Variance(bool isBiasCorrected, SecondMoment m2)
        {
            this.incMoment = false;
            this.moment = m2;
            this.isBiasCorrected = isBiasCorrected;
        }

        public Variance(Variance original) //throws NullArgumentException
        {
            Copy(original, this);
        }

        public void Increment(double d)
        {
            if (this.incMoment)
            {
                this.moment.Increment(d);
            }
        }

        public double GetResult()
        {
            if (this.moment.n == 0L)
            {
                return Double.NaN;
            }
            if (this.moment.n == 1L)
            {
                return 0.0D;
            }
            if (this.isBiasCorrected)
            {
                return this.moment.m2 / (this.moment.n - 1.0D);
            }
            return this.moment.m2 / this.moment.n;
        }

        public long GetN()
        {
            return this.moment.GetN();
        }

        public void Clear()
        {
            if (this.incMoment)
            {
                this.moment.Clear();
            }
        }

        public double Evaluate(double[] values) //throws MathIllegalArgumentException
        {
            if (values == null)
            {
                //throw new NullArgumentException(LocalizedFormats.INPUT_ARRAY, new Object[0]);
            }
            return Evaluate(values, 0, values.Length);
        }

        public double Evaluate(double[] values, int begin, int length) //throws MathIllegalArgumentException
        {
            double var = Double.NaN;
            if (Test(values, begin, length))
            {
                Clear();
                if (length == 1)
                {
                    var = 0.0D;
                }
                else if (length > 1)
                {
                    Mean mean = new Mean();
                    double m = mean.Evaluate(values, begin, length);
                    var = Evaluate(values, m, begin, length);
                }
            }
            return var;
        }

        public double Evaluate(double[] values, double[] weights, int begin, int length) //throws MathIllegalArgumentException
        {
            double var = Double.NaN;
            if (Test(values, weights, begin, length))
            {
                Clear();
                if (length == 1)
                {
                    var = 0.0D;
                }
                else if (length > 1)
                {
                    Mean mean = new Mean();
                    double m = mean.Evaluate(values, weights, begin, length);
                    var = Evaluate(values, weights, m, begin, length);
                }
            }
            return var;
        }

        public double Evaluate(double[] values, double[] weights) //throws MathIllegalArgumentException
        {
            return Evaluate(values, weights, 0, values.Length);
        }

        public double Evaluate(double[] values, double mean, int begin, int length) //throws MathIllegalArgumentException
        {
            double var = Double.NaN;
            if (Test(values, begin, length))
            {
                if (length == 1)
                {
                    var = 0.0D;
                }
                else if (length > 1)
                {
                    double accum = 0.0D;
                    double dev = 0.0D;
                    double accum2 = 0.0D;
                    for (int i = begin; i < begin + length; i++)
                    {
                        dev = values[i] - mean;
                        accum += dev * dev;
                        accum2 += dev;
                    }
                    double len = length;
                    if (this.isBiasCorrected)
                    {
                        var = (accum - accum2 * accum2 / len) / (len - 1.0D);
                    }
                    else
                    {
                        var = (accum - accum2 * accum2 / len) / len;
                    }
                }
            }
            return var;
        }

        public double Evaluate(double[] values, double mean) //throws MathIllegalArgumentException
        {
            return Evaluate(values, mean, 0, values.Length);
        }

        public double Evaluate(double[] values, double[] weights, double mean, int begin, int length) //throws MathIllegalArgumentException
        {
            double var = Double.NaN;
            if (Test(values, weights, begin, length))
            {
                if (length == 1)
                {
                    var = 0.0D;
                }
                else if (length > 1)
                {
                    double accum = 0.0D;
                    double dev = 0.0D;
                    double accum2 = 0.0D;
                    for (int i = begin; i < begin + length; i++)
                    {
                        dev = values[i] - mean;
                        accum += weights[i] * (dev * dev);
                        accum2 += weights[i] * dev;
                    }
                    double sumWts = 0.0D;
                    for (int i = begin; i < begin + length; i++)
                    {
                        sumWts += weights[i];
                    }
                    if (this.isBiasCorrected)
                    {
                        var = (accum - accum2 * accum2 / sumWts) / (sumWts - 1.0D);
                    }
                    else
                    {
                        var = (accum - accum2 * accum2 / sumWts) / sumWts;
                    }
                }
            }
            return var;
        }

        public double Evaluate(double[] values, double[] weights, double mean) //throws MathIllegalArgumentException
        {
            return Evaluate(values, weights, mean, 0, values.Length);
        }

        public bool IsBiasCorrected()
        {
            return this.isBiasCorrected;
        }

        public void SetBiasCorrected(bool biasCorrected)
        {
            this.isBiasCorrected = biasCorrected;
        }

        public Variance copy()
        {
            Variance result = new Variance();

            Copy(this, result);
            return result;
        }

        public static void Copy(Variance source, Variance dest) //throws NullArgumentException
        {
            MathUtils.checkNotNull(source);
            MathUtils.checkNotNull(dest);
            dest.SetData(source.GetDataRef());
            dest.moment = source.moment.Copy();
            dest.isBiasCorrected = source.isBiasCorrected;
            dest.incMoment = source.incMoment;
        }
    }
}
