using System;

namespace Arta.Tests
{
    class NotStationaryException : ArgumentException
    {
        public NotStationaryException(string message) : base(message)
        {
        }
    }
}
