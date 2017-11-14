using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Util
{
    public static class ExtensionMethods
    {
        public static double[] CopyOfRange(this System.Array array, int start, int end)
        {
            int len = end - start;
            double[] dest = new double[len];
            Array.Copy(array, start, dest, 0, len);
            return dest;
        }
    }
}
