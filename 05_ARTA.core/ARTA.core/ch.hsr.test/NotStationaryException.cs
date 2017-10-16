using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.test
{
    class NotStationaryException : Exception
    {
        public NotStationaryException()
        {
        }

        public NotStationaryException(string message)
        : base(message)
    {
        }

        public NotStationaryException(string message, Exception inner)
        : base(message, inner)
    {
        }
    }
}
