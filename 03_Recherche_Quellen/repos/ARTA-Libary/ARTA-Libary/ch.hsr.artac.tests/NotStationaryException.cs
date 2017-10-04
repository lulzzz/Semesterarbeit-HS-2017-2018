using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARTA_Libary.ch.hsr.artac.tests
{
    [Serializable]
    class NotStationaryException : Exception
    {
        public NotStationaryException() { }
        public NotStationaryException(string message) : base(message) { }
        public NotStationaryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
