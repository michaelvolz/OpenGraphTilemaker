using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Veondo.Common
{
	public static class Tools
	{
		/// <remarks>
		/// http://blogs.msdn.com/csharpfaq/archive/2010/01/06/getting-information-about-objects-types-and-members-with-expression-trees.aspx
		/// </remarks>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The e.</param>
		/// <returns></returns>
		public static MemberInfo MemberOf<T>( Expression<Func<T>> e )
		{
			Contract.Requires( e != null );
			Contract.Requires( e.Body != null );

			return MemberOf( e.Body );
		}

		// We need to add this overload to cover scenarios 
		// when a method has a void return type.
		public static MemberInfo MemberOf( Expression<Action> e )
		{
			Contract.Requires( e != null );
			Contract.Requires( e.Body != null );

			return MemberOf( e.Body );
		}

		private static MemberInfo MemberOf( Expression body )
		{
			Contract.Requires( body != null );

			{
				var member											= body as MemberExpression;
				if ( member != null )
					return member.Member;
			}

			{
				var method											= body as MethodCallExpression;
				if ( method != null )
					return method.Method;
			}

			throw new ArgumentException(
				"'" + body + "': not a member access" );
		}

		public static Version GetAssemblyVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version;
		}

		public static string GetAssemblyPath()
		{
			return Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );
		}
	}
}

