using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Common.Exceptions;

// ReSharper disable UnusedParameter.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    public static class DefaultGuard
    {
        public static T Default<T>(this IGuardClause guardClause, T input, string parameterName) {
            if (EqualityComparer<T>.Default.Equals(input, default)) {
                throw new GuardException(new ArgumentDefaultException(parameterName));
            }

            return input;
        }
    }

    public static class EnumGuard
    {
        public static void Enum(this IGuardClause guardClause, object input, Type enumClass, string parameterName) {
            if (!System.Enum.IsDefined(enumClass, input))
                throw new GuardException(new InvalidEnumArgumentException(parameterName, (int) input, enumClass));
        }
    }

    /// <summary>
    ///     Assertion should always be true or an exception will be thrown
    /// </summary>
    public static class AssertionGuard
    {
        public static void Assert(this IGuardClause guardClause, Expression<Func<bool>> input, string parameterName) {
            if (input.Compile().Invoke() == false) {
                throw new GuardException(new ArgumentException($"Assertion failed:  {input}.", parameterName));
            }
        }
    }

    public static class ConditionGuard
    {
        /// <summary>
        ///     Condition should always be false or an exception will be thrown
        /// </summary>
        public static void Condition(this IGuardClause guardClause, Expression<Func<bool>> input, string parameterName) {
            if (input.Compile().Invoke()) {
                throw new GuardException(new ArgumentException($"Condition reached:  {input}.", parameterName));
            }
        }
    }
}