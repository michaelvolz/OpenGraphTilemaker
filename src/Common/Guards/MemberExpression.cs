using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     MemberExpression.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static partial class GuardClauseExtensions
    {
        public static string MemberExpressionName<T>(this Expression<Func<T>> func) => 
            func.MemberExpression()?.Member.Name ?? "MemberExpression-ERROR";

        public static MemberExpression? MemberExpression<T>(this Expression<Func<T>> func) =>
            ((func.Body as UnaryExpression)?.Operand ?? func.Body) as MemberExpression;
    }
}