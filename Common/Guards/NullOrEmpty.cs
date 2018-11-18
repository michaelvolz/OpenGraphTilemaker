using System;
using System.Linq.Expressions;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable CheckNamespace

public static partial class GuardClauseExtensions
{
    /// <summary>
    ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
    ///     Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an empty string.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <returns>The input for variable initialization</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrEmpty(this IGuardClause guardClause, string input, string parameterName) {
        Guard.Against.Null(input, parameterName);
        if (input == string.Empty) {
            throw new GuardException(
                new ArgumentException("Value cannot be empty.", parameterName)
            );
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
    /// <returns>The input for variable initialization</returns>
    /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid</exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrEmpty(this IGuardClause guardClause, [NotNull] Expression<Func<string>> input) {
        return Guard.Against.NullOrEmpty(input.Compile()(), input.MemberExpressionName());
    }

}