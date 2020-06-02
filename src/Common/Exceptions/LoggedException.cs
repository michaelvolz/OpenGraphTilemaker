using System;
using System.Diagnostics.CodeAnalysis;

namespace Common.Exceptions
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LoggedException : Exception, ILoggedException
    {
        public LoggedException() { }
        public LoggedException(string message) : base(message) { }
        public LoggedException(string message, Exception innerException) : base(message, innerException) { }
        public LoggedException(Exception innerException) : base(null, innerException) { }
    }
}
