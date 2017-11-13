using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Tests
{
    class NotStationaryException : ArgumentException
    {
        public NotStationaryException(string message) : base(message)
        {
        }
    }
}
