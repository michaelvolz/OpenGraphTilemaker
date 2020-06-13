using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Assertion Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "Utility class")]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Utility class")]
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentException" /> if <see paramref="input" /> is false.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Assert(this IGuardClause guardClause, Expression<Func<bool>> input, string parameterName)
        {
            Guard.Against.Null(input, nameof(input));
            if (input.Compile().Invoke() == false) throw new GuardException(new ArgumentException($"Assertion failed:  {input}.", parameterName));
        }
    }
}
