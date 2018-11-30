using System;
using Common.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Local
#pragma warning disable CA1032 // Implement standard exception constructors

namespace Common.Exceptions
{
    public class LoggedException : Exception
    {
        public LoggedException(string message, string loggerName) : base(message)
            => LogException(message, null, loggerName);

        public LoggedException(string message, Exception innerException, string loggerName) : base(message, innerException)
            => LogException(message, innerException, loggerName);

        private void LogException(string message, Exception innerException, string loggerName) {
            var logger = ApplicationLogging.CreateLogger(loggerName);
            logger.LogError(innerException, message);
        }
    }
}
