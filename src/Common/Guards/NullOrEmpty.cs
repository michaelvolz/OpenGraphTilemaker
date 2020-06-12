using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     NullOrEmpty Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Utility class")]
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see paramref="input" /> is null.
        ///     Throws an <see cref="ArgumentException" /> if <see paramref="input" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrEmpty(this IGuardClause guardClause, string input, string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (string.IsNullOrEmpty(input)) throw new GuardException(new ArgumentException("Value cannot be empty.", parameterName));

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see paramref="input" /> is null.
        ///     Throws an <see cref="ArgumentException" /> if <see paramref="input" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrEmpty(this IGuardClause guardClause, Expression<Func<string>> input)
        {
            Guard.Against.Null(input, nameof(input));
            return Guard.Against.NullOrEmpty(input.Compile()(), input.MemberExpressionName());
        }
    }
}
