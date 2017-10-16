using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.fitting
{
    class OrderEstimator
    {
        public const int DEFAULT_MAX_ORDER = 100;

        public static int EstimateOrder(double[] data, int maxOrder)
        {
            double significanceLevel = 2 / Math.Sqrt(data.Length);
            int order = maxOrder;
            double[] acfs = AutocorrelationFitter.CalculateAcfs(data, maxOrder);
            double[] pacfs = AutocorrelationFitter.CalculatePacfs(acfs);
            for(int i = 0; i < maxOrder; i++)
            {
                if(Math.Abs(pacfs[i+1]) < significanceLevel)
                {
                    return i;
                }
            }
            return order;
        }

        public static int EstimateOrder(double[] data)
        {
            int maxOrder = Math.Min(data.Length / 4, DEFAULT_MAX_ORDER);
            return EstimateOrder(data, maxOrder);
        }
    }
}
