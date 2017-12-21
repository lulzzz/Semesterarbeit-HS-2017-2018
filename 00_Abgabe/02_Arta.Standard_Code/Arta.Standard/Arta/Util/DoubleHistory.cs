using System;

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
            values = new double[size];
            for(var i = 0; i < size; i++)
            {
                values[i] = 0.0;
            }
            addIndex = 0;
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
            var i = addIndex + size - index - 1;
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
