using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;

// ReSharper disable ArgumentsStyleLiteral
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     A collection of common guard clauses, implemented as extensions.
    /// </summary>
    /// <example>
    ///     Guard.Against.Null(input, nameof(input));
    /// </example>
    public static class GuardClauseExtensions
    {
        public static string RewindStackTraceMessage(this Exception exception) {
            StackTrace StackTrace() {
                var skipFrames = 0;

                if (exception.Message.StartsWith(GuardException.GuardPrefix, StringComparison.InvariantCultureIgnoreCase)) {
                    skipFrames = 1;
                }

                var stackTrace = new StackTrace(exception, skipFrames, fNeedFileInfo: true);

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


        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T Null<T>(this IGuardClause guardClause, T input, string parameterName) {
            if (null == input) {
                throw new GuardException(new ArgumentNullException(parameterName));
            }

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        ///     Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty(this IGuardClause guardClause, string input, string parameterName) {
            Guard.Against.Null(input, parameterName);
            if (input == string.Empty) {
                throw new GuardException(
                    new ArgumentException($"Required input {parameterName} was empty.", parameterName)
                );
            }
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        ///     Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an empty or white space string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrWhiteSpace(this IGuardClause guardClause, string input, string parameterName) {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input)) {
                throw new GuardException(new ArgumentException($"Required input {parameterName} was empty.", parameterName));
            }

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" />
        ///     or greater than <see cref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, int input, string parameterName, int rangeFrom, int rangeTo) {
            OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" />
        ///     or greater than <see cref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, DateTime input, string parameterName, DateTime rangeFrom, DateTime rangeTo) {
            OutOfRange<DateTime>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is not in the range of valid
        ///     <see cref="SqlDateTime" /> values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfSQLDateRange(this IGuardClause guardClause, DateTime input, string parameterName) {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));
        }

        private static void OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo) {
            var comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0) {
                throw new GuardException(
                    new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}")
                );
            }

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0) {
                throw new GuardException(
                    new ArgumentOutOfRangeException($"Input {parameterName} was out of range", parameterName)
                );
            }
        }
    }
}