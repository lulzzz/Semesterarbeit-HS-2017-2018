using ARTA.core.ch.hsr.util;
using Math3.distribution;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    class ArProcess
    {
        private readonly double[] alphas;
        private readonly ValueHistory<Double> values;
        private readonly NormalDistribution whiteNoiseProcess;

    /**
    * Generate a new ARTA-Process with the given parameters. 
    * */
        public ArProcess(double[] alphas, NormalDistribution whiteNoiseProcess)
        {
            this.alphas = alphas;
            this.values = new DoubleHistory(alphas.Length);
            this.whiteNoiseProcess = whiteNoiseProcess;
       }

        public double Next()
        {
            double value = whiteNoiseProcess.sample();
            for(int i = 0; i < alphas.Length; i++)
            {
                value = value + alphas[i] * values.get(i);
            }
            values.add(value);
            return value;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


}
