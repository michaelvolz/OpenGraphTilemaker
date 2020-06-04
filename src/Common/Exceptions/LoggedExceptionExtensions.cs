using System;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Common.Exceptions
{
    public static class LoggedExceptionExtensions
    {
        public static bool LogException<T>(this Exception e)
        {
            if (e is ILoggedExceptionAlready) return false;

            var logger = ApplicationLogging.CreateLogger<T>();
            logger.LogError(e, "Exception logged with Scope: ");

            return true;
        }
    }
}