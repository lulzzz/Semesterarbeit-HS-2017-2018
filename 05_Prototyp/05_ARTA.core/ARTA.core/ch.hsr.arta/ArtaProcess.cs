using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{
    interface IArtaProcess
    {
        double Next();
        ArProcess GetArProcess();
    }
}
