using System;
using System.Linq.Expressions;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     MemberExpression.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        public static string MemberExpressionName<T>(this Expression<Func<T>> func) =>
            func.MemberExpression()?.Member.Name ?? "MemberExpression-ERROR";

        private static MemberExpression? MemberExpression<T>(this Expression<Func<T>> func) =>
            ((Guard.Against.Null(func, nameof(func)).Body as UnaryExpression)?.Operand ?? func.Body) as MemberExpression;
    }
}
