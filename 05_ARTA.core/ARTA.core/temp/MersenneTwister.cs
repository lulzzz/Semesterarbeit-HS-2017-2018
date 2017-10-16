using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet
{
    class MersenneTwister : BitsStreamGenerator
    {
        private static readonly int N = 624;
        private static readonly int M = 397;
        private static readonly int[] MAG01 = { 0, -1727483681 };
        private int[] mt;
        private int mti;

        public MersenneTwister()
        {
            this.mt = new int['?'];
            setSeed(System.CurrenstTimeMillis() + System.identityHashCode(this));
        }

        public MersenneTwister(int seed)
        {
            this.mt = new int['?'];
            setSeed(seed);
        }
        public MersenneTwister(int[] seed)
        {
            this.mt = new int['?'];
            setSeed(seed);
        }
        public MersenneTwister(long seed)
        {
            this.mt = new int['?'];
            setSeed(seed);
        }

        public void setSeed(int seed)
        {
            long longMt = seed;
            this.mt[0] = ((int)longMt);
            for(this.mti = 1; this.mti < 624; this.mti += 1)
            {
                longMT = 1812433253L * (longMT ^ longMT >> 30) + this.mti & 0xFFFFFFFF;
                this.mt[this.mti] = ((int)longMT);
            }
            clear();
        }

        public void setSeed(int[] seed)
        {
            if (seed == null)
            {
                setSeed(System.currentTimeMillis() + System.identityHashCode(this));
                return;
            }
            setSeed(19650218);
            int i = 1;
            int j = 0;
            for (int k = Math.Max(624, seed.Length); k != 0; k--)
            {
                long l0 = this.mt[i] & 0x7FFFFFFF | (this.mt[i] < 0 ? 2147483648L : 0L);
                long l1 = this.mt[(i - 1)] & 0x7FFFFFFF | (this.mt[(i - 1)] < 0 ? 2147483648L : 0L);
                long l = (l0 ^ (l1 ^ l1 >> 30) * 1664525L) + seed[j] + j;
                this.mt[i] = ((int)(l & 0xFFFFFFFF));
                i++; j++;
                if (i >= 624)
                {
                    this.mt[0] = this.mt['?'];
                    i = 1;
                }
                if (j >= seed.Length)
                {
                    j = 0;
                }
            }
            for (int k = 623; k != 0; k--)
            {
                long l0 = this.mt[i] & 0x7FFFFFFF | (this.mt[i] < 0 ? 2147483648L : 0L);
                long l1 = this.mt[(i - 1)] & 0x7FFFFFFF | (this.mt[(i - 1)] < 0 ? 2147483648L : 0L);
                long l = (l0 ^ (l1 ^ l1 >> 30) * 1566083941L) - i;
                this.mt[i] = ((int)(l & 0xFFFFFFFF));
                i++;
                if (i >= 624)
                {
                    this.mt[0] = this.mt['?'];
                    i = 1;
                }
            }
            this.mt[0] = int.MinValue;

            clear();
        }
        public void setSeed(long seed)
        {
            setSeed(new int[] { (int)(seed >>> 32), (int)(seed & 0xFFFFFFFF) });
        }
        protected int next(int bits)
        {
            if (this.mti >= 624)
            {
                int mtNext = this.mt[0];
                for (int k = 0; k < 227; k++)
                {
                    int mtCurr = mtNext;
                    mtNext = this.mt[(k + 1)];
                    int y = mtCurr & 0x80000000 | mtNext & 0x7FFFFFFF;
                    this.mt[k] = (this.mt[(k + 397)] ^ y >>> 1 ^ MAG01[(y & 0x1)]);
                }
                for (int k = 227; k < 623; k++)
                {
                    int mtCurr = mtNext;
                    mtNext = this.mt[(k + 1)];
                    int y = mtCurr & 0x80000000 | mtNext & 0x7FFFFFFF;
                    this.mt[k] = (this.mt[(k + 65309)] ^ y >>> 1 ^ MAG01[(y & 0x1)]);
                }
                int y = mtNext & 0x80000000 | this.mt[0] & 0x7FFFFFFF;
                this.mt['?'] = (this.mt['?'] ^ y >>> 1 ^ MAG01[(y & 0x1)]);

                this.mti = 0;
            }
            int y = this.mt[(this.mti++)];

            y ^= y >>> 11;
            y ^= y << 7 & 0x9D2C5680;
            y ^= y << 15 & 0xEFC60000;
            y ^= y >>> 18;

            return y >>> 32 - bits;
        }
    }
}