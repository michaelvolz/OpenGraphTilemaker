using System;
using Common.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global
#pragma warning disable CA1040 // Avoid empty interfaces

namespace Common.Exceptions
{
    public class LoggedException : Exception, ILoggedException
    {
        public LoggedException() { }

        public LoggedException(string message) : base(message) { }

        public LoggedException(string message, Exception innerException) : base(message, innerException) { }

        public LoggedException(Exception innerException) : base(null, innerException) { }
    }

    public static class LoggedExceptionExtensions
    {
        public static bool LogException<T>(this Exception e) {
            if (e is ILoggedException) return false;

            var logger = ApplicationLogging.CreateLogger<T>();
            logger.LogError(e, "Exception logged with Scope: ");

            return true;
        }
    }

    public interface ILoggedException { }
}
