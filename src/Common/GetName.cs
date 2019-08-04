using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
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
            // If the method gets a lambda expression that is not a member access,
            // for example, () => x + y, an exception is thrown.
            if (e.Body is MemberExpression member)
                return member.Member.Name;

            throw new ArgumentException($"\'{e}\': is not a valid expression for this method");
        }

        // GetName.Of<T>( x => x.Property-name)
        public static string Of<T>(Expression<Func<T, object>> expression) => FindMemberOrNull(expression).Name;

        public static MemberInfo FindMemberOrNull(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return FindMemberOrNull(((UnaryExpression)expression).Operand);
                case ExpressionType.Lambda:
                    return FindMemberOrNull(((LambdaExpression)expression).Body);
                case ExpressionType.Call:
                    return ((MethodCallExpression)expression).Method;
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member;
                default:
                    throw new ArgumentException($"\'{expression}\': is not a valid expression for this method");
            }
        }
    }
}