using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.util
{
    internal interface ValueHistory<T>
    {
        T Get(int index);
        void Add(T element);
        int Size();
    }
}
