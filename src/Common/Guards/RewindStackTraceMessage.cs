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
        public static string RewindStackTraceMessage(this Exception exception) {
            StackTrace StackTrace() {
                var skipFrames = 0;

                if (exception.Message.StartsWith(GuardException.GuardPrefix, StringComparison.InvariantCultureIgnoreCase)) {
                    skipFrames = 1;
                }

                var stackTrace = new StackTrace(exception, skipFrames, true);

                stackTrace = ExcludeGuardFrames(stackTrace, skipFrames);

                return stackTrace;
            }

            StackTrace ExcludeGuardFrames(StackTrace stackTrace, int skipFrames) {
                while (stackTrace.ToString().Contains(nameof(GuardClauseExtensions))) {
                    skipFrames += 1;
                    stackTrace = new StackTrace(exception, skipFrames, true);
                }

                return stackTrace;
            }

            string Message() {
                var message = exception.Message + Environment.NewLine;
                var inner = exception.InnerException;

                while (inner != null) {
                    message += inner.Message + Environment.NewLine;
                    inner = inner.InnerException;
                }

                return message;
            }

            return Message() + StackTrace();
        }
    }
}
