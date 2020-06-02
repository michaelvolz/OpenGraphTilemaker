using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Condition Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentException" /> if <see paramref="input" /> is true.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Condition(this IGuardClause guardClause, Expression<Func<bool>> input, string parameterName)
        {
            if (input.Compile().Invoke()) throw new GuardException(new ArgumentException($"Condition reached:  {input}.", parameterName));
        }
    }
}