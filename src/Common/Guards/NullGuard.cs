using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute {}
    
    /// <summary>
    ///     Null Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
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

        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        // [return: NotNullIfNotNull("input")]
        // public static T Null<T>(this IGuardClause guardClause, [NotNullIfNotNull("input")] Expression<Func<T>>? input) =>
        //     Guard.Against.Null(input!.Compile()(), input.MemberExpressionName());
    }
}