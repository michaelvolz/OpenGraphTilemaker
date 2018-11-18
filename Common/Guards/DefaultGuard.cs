using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Exceptions;
using JetBrains.Annotations;

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

        /// <summary>
        ///     Throws an <see cref="ArgumentDefaultException" /> if <see cref="input" /> is the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <returns>The input for variable initialization</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid</exception>
        /// <exception cref="ArgumentDefaultException"></exception>
        public static T Default<T>(this IGuardClause guardClause, [NotNull] Expression<Func<T>> input) {
            return Guard.Against.Default(input.Compile()(), input.MemberExpressionName());
        }
    }
}