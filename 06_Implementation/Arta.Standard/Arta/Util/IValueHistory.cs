using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Util
{
    interface IValueHistory<T>
    {
        T Get(int index);
        void Add(T item);
        int Size();
    }
}
