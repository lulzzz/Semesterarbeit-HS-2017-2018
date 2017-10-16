using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    class ArtaProcessGeneral : AbstractArtaProcess
    {
        private readonly RealDistribution distribution;
	    private readonly NormalDistribution normal = new NormalDistribution();
        public ArtaProcessGeneral(ArProcess ar, RealDistribution distribution) : base(ar)
        {
            this.distribution = distribution;
            // TODO Auto-generated constructor stub
        }

         override protected double Transform(double value)
        {
            // Transform to uniform distribution U(0,1)
            double result = normal.CumulativeProbability(value);
            // Transform to desired distribution
            result = distribution.inverseCumulativeProbability(result);
            return result;
        }
        public override String ToString()
        {
            StringBuilder result = new StringBuilder("ARTA process \n");
            result.Append("Distribution = ").Append(distribution.ToString()).Append("\n");
            result.Append(GetArProcess());
            return result.ToString();

        }
    }
}
