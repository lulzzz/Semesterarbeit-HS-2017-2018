using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet
{
    class Well1024a : AbstractWell
    {
        private static readonly int K = 1024;
        private static readonly int M1 = 3;
        private static readonly int M2 = 24;
        private static readonly int M3 = 10;

        public Well1024a() : base(1024, 3, 24, 10)
        {
            
        }

        public Well1024a(int seed) : base(1024, 3, 24, 10, seed)
        {
        }

        public Well1024a(int[] seed) : base(1024, 3, 24, 10, seed)
        {

        }

        public Well1024a(long seed) : base(1024, 3, 24, 10, seed)
        {

        }

        protected int Next(int bits)
        {
            int indexRm1 = this.iRm1[this.index];

            int v0 = this.v[this.index];
            int vM1 = this.v[this.i1[this.index]];
            int vM2 = this.v[this.i2[this.index]];
            int vM3 = this.v[this.i3[this.index]];

            int z0 = this.v[indexRm1];
            int z1 = v0 ^ vM1 ^ vM1 >>> 8;
            int z2 = vM2 ^ vM2 << 19 ^ vM3 ^ vM3 << 14;
            int z3 = z1 ^ z2;
            int z4 = z0 ^ z0 << 11 ^ z1 ^ z1 << 7 ^ z2 ^ z2 << 13;

            this.v[this.index] = z3;
            this.v[indexRm1] = z4;
            this.index = indexRm1;

            return z4 >>> 32 - bits;
        }
    }
}
