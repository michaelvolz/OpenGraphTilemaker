using System;
using Ardalis.GuardClauses;

// ReSharper disable UnusedParameter.Global
// ReSharper disable CheckNamespace

public static partial class GuardClauseExtensions
{
    /// <summary>
    ///     Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <returns>The input for variable initialization</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T Null<T>(this IGuardClause guardClause, T input, string parameterName) {
        if (null == input) {
            throw new GuardException(
                new ArgumentNullException(parameterName)
            );
        }

        return input;
    }
}