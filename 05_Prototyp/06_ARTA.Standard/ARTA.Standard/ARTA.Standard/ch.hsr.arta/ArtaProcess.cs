using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.arta
{

    public interface IArtaProcess
    {
        double Next();
        ArProcess GetArProcess();
    }
}
