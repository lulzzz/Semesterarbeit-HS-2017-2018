using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.util
{
    internal class DoubleHistory : ValueHistory<Double>
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
        public void Add(double value)
        {
            values[addIndex] = value;
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
                i -= size;
            }
            return values[i];
        }

        public int Size()
        {
            return size;
        }

        public static void Main()
        {
            DoubleHistory test = new DoubleHistory(3);
            test.Add(1.0);
            test.Add(2.0);
            test.Add(3.0);
            test.Add(4.0);
            for (int i = 0; i < test.Size(); i++)
            {
                Console.WriteLine(test.Get(i) + "\t");
            }
        }
    }
}
