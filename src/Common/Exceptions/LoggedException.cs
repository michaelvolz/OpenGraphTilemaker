using System;
using Common.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global
#pragma warning disable CA1032 // Implement standard exception constructors

namespace Common.Exceptions
{
    public class LoggedException<T> : Exception, ILoggedException
    {
        public LoggedException(string message, object propertyValue0 = null, object propertyValue1 = null, object propertyValue2 = null)
            : base(message)
            => LogException(null, message, propertyValue0, propertyValue1, propertyValue2);

        public LoggedException(Exception innerException, string message, object propertyValue0 = null, object propertyValue1 = null,
            object propertyValue2 = null)
            : base(message, innerException) =>
            LogException(innerException, message, propertyValue0, propertyValue1, propertyValue2);

        private void LogException<T0, T1, T2>(Exception innerException, string message,
            T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) {
            var logger = ApplicationLogging.CreateLogger<T>();
            logger.LogError(innerException, message, propertyValue0, propertyValue1, propertyValue2);
        }
    }

    public interface ILoggedException { }
}
