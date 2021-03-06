﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Common.Exceptions;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Default Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentDefaultException" /> if <see paramref="input" /> is the default value.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentDefaultException"></exception>
        public static T Default<T>(this IGuardClause guardClause, T input, string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default!)) throw new GuardException(new ArgumentDefaultException(parameterName));

            return input;
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentDefaultException" /> if <see paramref="input" /> is the default value.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <returns>The input for variable initialization.</returns>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="ArgumentDefaultException"></exception>
        public static T Default<T>(this IGuardClause guardClause, Expression<Func<T>> input)
        {
            Guard.Against.Null(input, nameof(input));
            return Guard.Against.Default(input.Compile()(), input.MemberExpressionName());
        }
    }
}
