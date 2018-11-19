using System;
using System.Linq.Expressions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     MemberExpression.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        public static string MemberExpressionName<T>(this Expression<Func<T>> func) {
            return func.MemberExpression().Member.Name;
        }

        public static MemberExpression MemberExpression<T>(this Expression<Func<T>> func) {
            if (!(func.Body is MemberExpression member))
                throw new ArgumentException($"'{func}'  is not a valid expression for this method.");

            return member;
        }
    }
}