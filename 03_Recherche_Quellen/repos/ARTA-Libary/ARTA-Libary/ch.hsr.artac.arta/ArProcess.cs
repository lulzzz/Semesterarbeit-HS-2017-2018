using ARTA_Libary.ch.hsr.artac.util;
using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.arta
{
    internal class ArProcess
    {
        private readonly double[] alphas;
        private readonly ValueHistory<Double> vals;
        private readonly NormalDistribution whiteNoiseProcess;

        public ArProcess(double[] alphas, NormalDistribution whiteNoiseProcess)
        {
            this.alphas = alphas;
            this.vals = new DoubleHistory(alphas.Length);
            this.whiteNoiseProcess = whiteNoiseProcess;
        }

        public double Next()
        {
            double value = whiteNoiseProcess.sample();
            for ( int i = 0; i < alphas.Length; i++)
            {
                value += alphas[i] * vals.Get(i);
            }
            vals.Add(value);
            return value;
        }

        public override string ToString()
        {
            return "AR-Process \n alphas   = " 
                + alphas.ToString() 
                + "\n variance = " 
                + (whiteNoiseProcess.getNumericalVariance());
        }
    }
}
