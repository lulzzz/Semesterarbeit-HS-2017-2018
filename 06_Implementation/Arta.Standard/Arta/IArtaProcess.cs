using System;
using System.Collections.Generic;
using System.Text;

namespace Arta
{
    public interface IArtaProcess
    {
        /*
         *Generates and returns the next Arta-Value
         */
        double Next();

        /*
         * returns the underlying Ar-Process
         */
        ArProcess GetArProcess();
    }
}
