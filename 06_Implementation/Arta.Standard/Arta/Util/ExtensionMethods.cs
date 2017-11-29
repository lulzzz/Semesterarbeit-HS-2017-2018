using System;

namespace Arta.Util
{
    public static class ExtensionMethods
    {
        public static double[] CopyOfRange(this Array array, int start, int end)
        {
            var len = end - start;
            var dest = new double[len];
            Array.Copy(array, start, dest, 0, len);
            return dest;
        }
    }
}
