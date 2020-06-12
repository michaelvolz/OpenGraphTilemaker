using System;
using System.Diagnostics.CodeAnalysis;
using Common.Guards;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Null Guard.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T Null<T>(this IGuardClause guardClause, [ValidatedNotNull] [NotNullIfNotNull("input")] T input, string parameterName)
        {
            if (input == null) throw new GuardException(new ArgumentNullException(parameterName));

            return input;
        }
    }
}
