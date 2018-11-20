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
            return func.MemberExpression()?.Member.Name ?? "MemberExpression-ERROR";
        }

        public static MemberExpression MemberExpression<T>(this Expression<Func<T>> func) {
            var member = func.Body as MemberExpression;
            var unary = func.Body as UnaryExpression;
            return member ?? (unary != null ? unary.Operand as MemberExpression : null);
        }
    }
}
