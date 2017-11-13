using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Util
{
    class DoubleHistory : IValueHistory<Double>
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

        public void Add(double item)
        {
            values[addIndex] = item;
            addIndex++;
            if(addIndex >= size)
            {
                addIndex = 0;
            }
        }

        public double Get(int index)
        {
            int i = addIndex + size - index - 1;
            if(i >= size)
            {
                i = i - size;
            }
            return values[i];
        }

        public int Size()
        {
            return size;
        }
    }
}
