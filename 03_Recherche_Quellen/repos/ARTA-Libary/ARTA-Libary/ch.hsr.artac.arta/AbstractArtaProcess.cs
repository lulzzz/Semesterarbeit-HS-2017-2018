using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.arta
{
    internal abstract class AbstractArtaProcess : ArtaProcess
    {
        private readonly ArProcess ar;

        AbstractArtaProcess(ArProcess ar)
        {
            this.ar = ar;
        }

        abstract protected double transfomr(double value);
    }
}
