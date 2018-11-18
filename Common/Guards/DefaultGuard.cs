using System.Collections.Generic;
using Common.Exceptions;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentDefaultException" /> if <see cref="input" /> is the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization</returns>
        /// <exception cref="ArgumentDefaultException"></exception>
        public static T Default<T>(this IGuardClause guardClause, T input, string parameterName) {
            if (EqualityComparer<T>.Default.Equals(input, default)) {
                throw new GuardException(new ArgumentDefaultException(parameterName));
            }

            return input;
        }
    }
}