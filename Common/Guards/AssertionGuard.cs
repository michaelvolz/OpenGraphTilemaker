using System;
using System.Linq.Expressions;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentException" /> if <see cref="input" /> is false.
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