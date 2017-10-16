using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.util
{
    class DoubleHistory : ValueHistory<Double>
    {
        private readonly int size;
        private readonly double[] values;
        private int addIndex;

        public DoubleHistory(int size)
        {
            this.size = size;
            this.values = new double[size];
            for(int i = 0; i < size; i++)
            {
                values[i] = 0.0;
            }
            this.addIndex = 0;
        }

        public void add(double value)
        {
            values[addIndex] = value;
            addIndex++;
            if (addIndex >= size)
            {
                addIndex = 0;
            }
        }

        public double get(int index)
        {
            int i = addIndex + size - index - 1;
            if (i >= size)
            {
                i = i - size;
            }
            return values[i];
        }

        int ValueHistory<double>.size()
        {
            return size;
        }
    }
}
