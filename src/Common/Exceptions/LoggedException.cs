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
        public LoggedException(Type loggerType, string message, params object[] args)
            : base(string.Format(message, args))
            => LogException(loggerType, null, message, args);

        public LoggedException(Type loggerType, Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException) =>
            LogException(loggerType, innerException, message, args);

        private void LogException(Type loggerType, Exception innerException, string message, params object[] args) {
            var logger = ApplicationLogging.CreateLogger(loggerType);
            logger.LogError(innerException, message, args);
        }
    }
}
