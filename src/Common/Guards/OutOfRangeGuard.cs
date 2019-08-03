using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using JetBrains.Annotations;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     A collection of common guard clauses, implemented as extensions.
    /// </summary>
    /// <example>
    ///     Guard.Against.Null(input, nameof(input));.
    /// </example>
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see paramref="input" /> is less than <see paramref="rangeFrom" />
        ///     or greater than <see paramref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int OutOfRange(this IGuardClause guardClause, int input, string parameterName, int rangeFrom, int rangeTo) {
            OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see paramref="input" /> is less than <see paramref="rangeFrom" />
        ///     or greater than <see paramref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int OutOfRange(this IGuardClause guardClause,  Expression<Func<int>> input, int rangeFrom, int rangeTo) {
            return OutOfRange<int>(guardClause, input.Compile()(), input.MemberExpressionName(), rangeFrom, rangeTo);
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see paramref="input" /> is less than <see paramref="rangeFrom" />
        ///     or greater than <see paramref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfRange(this IGuardClause guardClause, DateTime input, string parameterName, DateTime rangeFrom, DateTime rangeTo) {
            OutOfRange<DateTime>(guardClause, input, parameterName, rangeFrom, rangeTo);

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see paramref="input" /> is less than <see paramref="rangeFrom" />
        ///     or greater than <see paramref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfRange(this IGuardClause guardClause,  Expression<Func<DateTime>> input, DateTime rangeFrom, DateTime rangeTo) {
            return OutOfRange<DateTime>(guardClause, input.Compile()(), input.MemberExpressionName(), rangeFrom, rangeTo);
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> if <see paramref="input" /> is not in the range of valid
        ///     <see cref="SqlDateTime" /> values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfSQLDateRange(this IGuardClause guardClause, DateTime input, string parameterName) {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));

            return input;
        }

        private static T OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo) {
            var comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0) {
                throw new GuardException(new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}"));
            }

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0) {
                throw new GuardException(new ArgumentOutOfRangeException($"Input {parameterName} was out of range", parameterName));
            }

            return input;
        }
    }
}
