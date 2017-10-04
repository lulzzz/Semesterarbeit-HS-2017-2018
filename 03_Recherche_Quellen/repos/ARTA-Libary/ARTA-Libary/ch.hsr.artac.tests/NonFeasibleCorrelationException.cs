using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.tests
{
    class NonFeasibleCorrelationException : Exception
    {
        public NonFeasibleCorrelationException() { }
        public NonFeasibleCorrelationException(string message) : base(message) { }
        public NonFeasibleCorrelationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
