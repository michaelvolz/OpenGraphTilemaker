using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Common.Exceptions
{
    /// <summary>
    ///     https://blog.stephencleary.com/2020/06/a-new-pattern-for-exception-logging.html
    /// </summary>
    [UsedImplicitly]
    public static class ExceptionExtensions
    {
        [UsedImplicitly]
        private static object? Examples(ILogger logger)
        {
            // Log-and-propagate, new pattern:
            try
            {
                // ...
            }
            catch (Exception e) when (False(() => logger.LogError(e, "Unexpected error.")))
            {
                throw;
            }

            // Log-and-handle, new pattern:
            try
            {
                // ...
            }
            catch (Exception e) when (True(() => logger.LogError(e, "Unexpected error.")))
            {
                return null; // or some other handling code
            }

            return default;
        }

        // Use when you want to handle the exception
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Exception-handling")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
        public static bool True(Action action)
        {
            action();
            return true;
        }

        // Use when you want to propagate the exception
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Exception-handling")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
        public static bool False(Action action)
        {
            action();
            return false;
        }
    }
}
