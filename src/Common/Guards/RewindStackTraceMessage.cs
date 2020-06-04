using System;
using System.Diagnostics;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     RewindStackTraceMessage.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Rewinds the <see cref="StackTrace" /> to remove the <see cref="GuardClauseExtensions" /> code from it.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Trimmed <see cref="StackTrace" />.</returns>
        public static string RewindStackTraceMessage(this Exception exception)
        {
            StackTrace StackTrace()
            {
                var skipFrames = 0;
                StackTrace stackTrace;

                do
                {
                    stackTrace = new StackTrace(exception, skipFrames, true);
                    skipFrames += 1;
                } while (stackTrace.ToString().Contains(nameof(GuardClauseExtensions), StringComparison.InvariantCultureIgnoreCase));

                return stackTrace;
            }

            string Message()
            {
                var message = exception.Message + Environment.NewLine;
                var inner = exception.InnerException;

                while (inner != null)
                {
                    message += inner.Message + Environment.NewLine;
                    inner = inner.InnerException;
                }

                return message;
            }

            return Message() + StackTrace();
        }
    }
}