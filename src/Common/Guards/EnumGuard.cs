﻿using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     EnumGuard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "Utility class")]
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        ///     Throws an <see cref="InvalidEnumArgumentException" /> if <see paramref="input" /> is not a valid value for the
        ///     defined <see paramref="enumClass" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="enumClass"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void Enum(this IGuardClause guardClause, object input, Type enumClass, string parameterName)
        {
            if (!System.Enum.IsDefined(enumClass, input))
                throw new GuardException(new InvalidEnumArgumentException(parameterName, (int)input, enumClass));
        }

        /// <summary>
        ///     Throws an <see cref="InvalidEnumArgumentException" /> if <see paramref="input" /> is not a valid value for the
        ///     defined
        ///     <see paramref="enumClass" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="enumClass"></param>
        /// <exception cref="ArgumentException">The <paramref name="input" /> expression is invalid.</exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void Enum<T>(this IGuardClause guardClause, Expression<Func<T>> input, Type enumClass)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.Enum(input.Compile()() ?? throw new InvalidOperationException(), enumClass, input.MemberExpressionName());
        }
    }
}
