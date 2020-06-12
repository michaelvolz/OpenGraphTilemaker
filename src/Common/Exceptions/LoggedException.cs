using System;
using System.Diagnostics.CodeAnalysis;

namespace Common.Exceptions
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Exception format")]
    public class LoggedException : Exception, ILoggedExceptionAlready
    {
        public LoggedException() { }
        public LoggedException(string message) : base(message) { }
        public LoggedException(string message, Exception innerException) : base(message, innerException) { }
        public LoggedException(Exception innerException) : base(null, innerException) { }
    }
}
