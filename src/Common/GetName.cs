using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Ardalis.GuardClauses;

namespace Common
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class GetName
    {
        // GetName.Of<Classname>()
        public static string Of<T>() where T : class => typeof(T).Name;

        // GetName.Of( () => variable-name)
        // GetName.Of( () => class.Property-name)

        /// <remarks>
        ///     http://blogs.msdn.com/csharpfaq/archive/2010/01/06/getting-information-about-objects-types-and-members-with-expression-trees.aspx.
        /// </remarks>
        public static string Of<T>(Expression<Func<T>> e)
        {
            Guard.Against.Null(e, nameof(e));

            // If the method gets a lambda expression that is not a member access,
            // for example, () => x + y, an exception is thrown.
            if (e.Body is MemberExpression member)
                return member.Member.Name;

            throw new ArgumentException($"\'{e}\': is not a valid expression for this method");
        }

        // GetName.Of<T>( x => x.Property-name)
        public static string Of<T>(Expression<Func<T, object>> expression) => FindMemberOrNull(Guard.Against.Null(expression, nameof(expression))).Name;

        private static MemberInfo FindMemberOrNull(Expression expression) =>
            expression.NodeType switch
            {
                ExpressionType.Convert => FindMemberOrNull(((UnaryExpression)expression).Operand),
                ExpressionType.Lambda => FindMemberOrNull(((LambdaExpression)expression).Body),
                ExpressionType.Call => ((MethodCallExpression)expression).Method,
                ExpressionType.MemberAccess => ((MemberExpression)expression).Member,
                _ => throw new ArgumentException($"\'{expression}\': is not a valid expression for this method")
            };
    }
}