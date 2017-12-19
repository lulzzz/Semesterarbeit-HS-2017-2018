using System;

namespace Arta.Verification
{
    class NotStationaryException : ArgumentException
    {
        public NotStationaryException(string message) : base(message)
        {
        }
    }
}
