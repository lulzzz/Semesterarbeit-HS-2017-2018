using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    public abstract class AbstractArtaProcess : IArtaProcess
    {
        private readonly ArProcess ar;
        public AbstractArtaProcess(ArProcess ar)
        {
            this.ar = ar;
        }

        public ArProcess GetArProcess()
        {
            return ar;
        }

        public double Next()
        {
            return Transform(ar.Next());
        }

        abstract protected double Transform(double value);
        
    }
}
