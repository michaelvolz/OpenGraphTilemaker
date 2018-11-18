using System;

// ReSharper disable UnusedParameter.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        ///     Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an empty or white space string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrWhiteSpace(this IGuardClause guardClause, string input, string parameterName) {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input)) {
                throw new GuardException(new ArgumentException("Value cannot be whitespace.", parameterName));
            }

            return input;
        }
    }
}