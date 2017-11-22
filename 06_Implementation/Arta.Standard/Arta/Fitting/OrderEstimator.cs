using Arta.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Fitting
{
    public class OrderEstimator
    {
        public const int Default_Max_Order = 100;
        public static int EstimateOrder(double[] data, int maxOrder)
        {
            var significanceLevel = 2 / System.Math.Sqrt(data.Length);
            var order = maxOrder;
            double[] acfs = AutoCorrelation.CalculateAcfs(data, maxOrder);
            double[] pacfs = AutoCorrelation.CalculatePacfs(acfs);

            for (var i = 0; i < maxOrder; i++)
            {
                if (System.Math.Abs(pacfs[i + 1]) < significanceLevel)
                {
                    return i;
                }
            }
            return order;
        }

        public static int EstimateOrder(double[] data)
        {
            var maxOrder = System.Math.Min(data.Length / 4, Default_Max_Order);
            return EstimateOrder(data, maxOrder);
        }
    }
}
