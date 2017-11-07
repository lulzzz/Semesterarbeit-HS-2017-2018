using ARTA.core.ch.hsr.arta;
using ARTA.core.ch.hsr.fitting;
using ARTA.core.ch.hsr.math;
using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.Standard.ch.hsr.example
{
    class ArtaExample
    {
        public static void Main(String[] args){



        RealDistribution distribution = new ExponentialDistribution(1.0);
        double[] artaCorrelationCoefficients = { 0.3, 0.3, -0.1 };
        IArtaProcess arta = ArtaProcessFactory.CreateArtaProcess(distribution, artaCorrelationCoefficients);

        double[] data = new double[10000];
		for (int i = 0; i<data.Length; i++) {
			data[i] = arta.Next();
		}


    int maxLag = 10;
    double[] acfs = AutoCorrelation.CalculateAcfs(data, maxLag);
    double[] pacfs = AutoCorrelation.CalculatePacfs(acfs);

            Console.WriteLine("#########################");
            Console.WriteLine("ACFS");
            foreach(double d in acfs){
                Console.WriteLine(d.ToString());
            }
            Console.WriteLine('\n');

            Console.WriteLine("#########################");
            Console.WriteLine("PACFS");
            foreach(double d in pacfs)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine('\n');

            Console.WriteLine("#########################");
            Console.WriteLine("Order");
            Console.WriteLine(OrderEstimator.EstimateOrder(data, maxLag));
}
}
}
