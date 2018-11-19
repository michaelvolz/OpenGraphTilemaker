using System;
using System.Linq.Expressions;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable CheckNamespace
#pragma warning disable SA1614 // Element parameter documentation should have text
#pragma warning disable SA1627 // Documentation text should not be empty

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Assertion Guard.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentException" /> if <see paramref="input" /> is false.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Assert(this IGuardClause guardClause, Expression<Func<bool>> input, string parameterName) {
            if (input.Compile().Invoke() == false) {
                throw new GuardException(new ArgumentException($"Assertion failed:  {input}.", parameterName));
            }
        }
    }
}

#pragma warning restore SA1614 // Element parameter documentation should have text
#pragma warning restore SA1627 // Documentation text should not be empty