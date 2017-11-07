using Math3.random;
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
            long numOfTicks = DateTime.Now.Ticks;
            long numOfMilliseconds = numOfTicks / 10000;
            this.mt = new int['?'];
            setSeed(numOfMilliseconds +  this.GetHashCode());
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

        public override void setSeed(int seed)
        {
            long longMt = seed;
            this.mt[0] = ((int)longMt);
            for(this.mti = 1; this.mti < 624; this.mti += 1)
            {
                longMt = 1812433253L * (longMt ^ longMt >> 30) + this.mti & 0xFFFFFFFF;
                this.mt[this.mti] = ((int)longMt);
            }
            clear();
        }

        public override void setSeed(int[] seed)
        {
            if (seed == null)
            {
                setSeed(GetMilliSec() + this.GetHashCode());
                return;
            }
            setSeed(19650218);
            int i = 1;
            int j = 0;
            for (int k = Math.Max(624, seed.Length); k != 0; k--)
            {
                long l0 = mt[i] & 0x7FFFFFFF | (mt[i] < 0 ? 2147483648L : 0L);
                long l1 = mt[(i - 1)] & 0x7FFFFFFF | (mt[(i - 1)] < 0 ? 2147483648L : 0L);
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
                long l0 = mt[i] & 0x7FFFFFFF | (mt[i] < 0 ? 2147483648L : 0L);
                long l1 = mt[(i - 1)] & 0x7FFFFFFF | (mt[(i - 1)] < 0 ? 2147483648L : 0L);
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
        public override void setSeed(long seed)
        {
            setSeed(new int[] { (int)(seed >> 32), (int)(seed & 0xFFFFFFFF) });
        }
        protected override int next(int bits)
        {
            if (this.mti >= 624)
            {
                int mtNext = this.mt[0];
                for (int k = 0; k < 227; k++)
                {
                    int mtCurr = mtNext;
                    mtNext = this.mt[(k + 1)];
                    int s = mtCurr & int.MinValue | mtNext & int.MaxValue;
                    this.mt[k] = (this.mt[(k + 397)] ^  s >> 1 ^ MAG01[(s & 0x1)]);
                }
                for (int k = 227; k < 623; k++)
                {
                    int mtCurr = mtNext;
                    mtNext = this.mt[(k + 1)];
                    int b = mtCurr & int.MinValue | mtNext & int.MaxValue;
                    this.mt[k] = (this.mt[(k + 65309)] ^ b >> 1 ^ MAG01[(b & 0x1)]);
                }
                int t = mtNext & int.MinValue | mt[0] & int.MaxValue;
                this.mt['?'] = (mt['?'] ^ t >> 1 ^ MAG01[(t & 0x1)]);

                this.mti = 0;
            }
            int y = this.mt[(this.mti++)];

            y ^= y >> 11;
            y = y ^ (y << 7 & -1658038656);//0x9D2C5680;
            y ^= y << 15 & -272236544; //0xEFC60000
            y ^= y >> 18;

            return y >> 32 - bits;
        }

        private long GetMilliSec()
        {
            long numOfTicks = DateTime.Now.Ticks;
            long numOfMilliseconds = numOfTicks / 10000;
            return numOfMilliseconds;
        }
    }
}