using Math3.random;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.random
{
    class RandomAdaptor : Random, RandomGenerator
    {
        public RandomGenerator randomGenerator;
        MersenneTwister mersenneTwister;

        private RandomAdaptor()
        {
            this.randomGenerator = null;
        }

        public RandomAdaptor(RandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public RandomAdaptor(MersenneTwister mersenneTwister)
        {
            this.mersenneTwister = mersenneTwister;
        }

        public static Random CreateAdaptor(RandomGenerator randomGenerator)
        {
            return new RandomAdaptor(randomGenerator);
        }

        public bool NextBoolean()
        {
            return this.randomGenerator.nextBoolean();
        }

        public void nextBytes(byte[] bytes)
        {
            this.randomGenerator.nextBytes(bytes);
        }

        public double nextDouble()
        {
            return this.randomGenerator.nextDouble();
        }

        public float nextFloat()
        {
            return this.randomGenerator.nextFloat();
        }

        public double nextGaussian()
        {
            return this.randomGenerator.nextGaussian();
        }

        public int nextInt()
        {
            return this.randomGenerator.nextInt();
        }

        public int nextInt(int n)
        {
            return this.randomGenerator.nextInt(n);
        }

        public long nextLong()
        {
            return this.randomGenerator.nextLong();
        }

        public void setSeed(int seed)
        {
            if (this.randomGenerator != null)
            {
                this.randomGenerator.setSeed(seed);
            }
        }

        public void setSeed(int[] seed)
        {
            if (this.randomGenerator != null)
            {
                this.randomGenerator.setSeed(seed);
            }
        }

        public void setSeed(long seed)
        {
            if (this.randomGenerator != null)
            {
                this.randomGenerator.setSeed(seed);
            }
        }

        bool RandomGenerator.nextBoolean()
        {
            throw new NotImplementedException();
        }
    }
}
