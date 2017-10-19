using Math3.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.ArtaExtensions
{
    public static class StatHelper
    {

        public static double Mean(this double[] values)
        {
            double s = 0;
            for(int i = 0; i < values.Length; i++)
            {
                s += values[i];
            }

            return s / (values.Length);
        }

        public static double Variance(this double[] values)
        {
            double mean = values.Mean();
            double temp = 0;
            foreach(double d in values)
            {
                temp += (d - mean) * (d - mean);
            }
            return temp / (values.Length - 1);
        }

    }
}
