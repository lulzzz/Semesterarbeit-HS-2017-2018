using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.util
{
    interface ValueHistory<T>
    {
        T get(int index);
        void add(T element);
        int size();
    }
}
