using System;
using System.Collections.Generic;
using System.Text;

namespace ARTA.core.ch.hsr.test
{
    class NonFeasibleCorrelationException : Exception
    {
        public NonFeasibleCorrelationException()
        {
        }

        public NonFeasibleCorrelationException(string message)
        : base(message)
    {
        }

        public NonFeasibleCorrelationException(string message, Exception inner)
        : base(message, inner)
    {
        }

    }
}
