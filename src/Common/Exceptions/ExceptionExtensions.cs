using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Common.Exceptions
{
    /// <summary>
    ///     https://blog.stephencleary.com/2020/06/a-new-pattern-for-exception-logging.html
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Utility class")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Utility class")]
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Exception-handling")]
    public static class ExceptionExtensions
    {
        // Use when you want to handle the exception
        public static bool True(Action action)
        {
            action();
            return true;
        }

        // Use when you want to propagate the exception
        public static bool False(Action action)
        {
            action();
            return false;
        }

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
    }
}
