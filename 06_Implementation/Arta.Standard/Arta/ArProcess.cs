using Arta.Util;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    public class ArProcess
    {
        private readonly double[] alphas;
        private readonly IValueHistory<Double> vals;
        private readonly Normal whiteNoiseProcess;

        public ArProcess(double[] alphas, Normal whiteNoiseProcess)
        {
            this.alphas = alphas;
            this.whiteNoiseProcess = whiteNoiseProcess;
            vals = new DoubleHistory(alphas.Length);
        }

        public double Next()
        {
            double value = whiteNoiseProcess.Sample();
            for(var i = 0; i < alphas.Length; i++)
            {
                value = value + alphas[i] * vals.Get(i);
            }
            vals.Add(value);
            return value;
        }
    }
}
