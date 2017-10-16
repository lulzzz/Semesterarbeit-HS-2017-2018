using System;
using System.Collections.Generic;
using System.Text;

namespace Math3.random
{
    public class RandomAdaptor : Random, RandomGenerator
    {
        private RandomGenerator generator;

        public RandomAdaptor(RandomGenerator randomGenerator)
        {
            generator = randomGenerator;
        }

        public bool nextBoolean()
        {
            throw new NotImplementedException();
        }

        public void nextBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public double nextDouble()
        {
            return generator.nextDouble();
        }

        public float nextFloat()
        {
            throw new NotImplementedException();
        }

        public double nextGaussian()
        {
            throw new NotImplementedException();
        }

        public int nextInt()
        {
            throw new NotImplementedException();
        }

        public int nextInt(int n)
        {
            throw new NotImplementedException();
        }

        public long nextLong()
        {
            throw new NotImplementedException();
        }

        public void setSeed(int seed)
        {
            throw new NotImplementedException();
        }

        public void setSeed(int[] seed)
        {
            throw new NotImplementedException();
        }

        public void setSeed(long seed)
        {
            throw new NotImplementedException();
        }
    }
}
