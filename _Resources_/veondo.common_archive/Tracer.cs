using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Veondo.Common
{
	public static class Tracer
	{
		private const string BaseNameSpace							= "Veondo";
		public static TraceSource TraceSource						= new TraceSource( BaseNameSpace );

		[Conditional( "TRACE" )]
		public static void Write()
		{
			var methodBase											= GetMethodBase( 2 );
			WriteCore( String.Empty, methodBase );
		}

		[Conditional( "TRACE" )]
		public static void Write( string text )
		{
			Contract.Requires( text != null );

			var methodBase											= GetMethodBase( 2 );
			WriteCore( text, methodBase );
		}

		[Conditional( "TRACE" )]
		public static void Write( string format, params object[] args )
		{
			Contract.Requires( format != null );
			Contract.Requires( args != null );
			Contract.Requires( args.Length > 0 );

			var methodBase											= GetMethodBase( 2 );

			if ( format != null && args != null )
				WriteCore( String.Format( format, args ), methodBase );
		}

		[Conditional( "TRACE" )]
		public static void Warn()
		{
			var methodBase											= GetMethodBase( 2 );
			WarnCore( String.Empty, methodBase );
		}

		[Conditional( "TRACE" )]
		public static void Warn( string text )
		{
			Contract.Requires( text != null );
	
			var methodBase											= GetMethodBase( 2 );
			WarnCore( text, methodBase );
		}

		[Conditional( "TRACE" )]
		public static void Warn( string format, params object[] args )
		{
			Contract.Requires( format != null );
			Contract.Requires( args != null );
			Contract.Requires( args.Length > 0 );

			var methodBase											= GetMethodBase( 2 );

			if ( format != null && args != null )
				WarnCore( String.Format( format, args ), methodBase );
		}

		public static void WriteCore( string text, MethodBase methodBase )
		{
			Contract.Requires( text != null );
			Contract.Requires( methodBase != null );
			Contract.Requires( methodBase.Name != null );

			MyCtx.Write( methodBase.Name, TrimText( text, methodBase ) );
			TraceSource.TraceEvent( TraceEventType.Information, 0, "{1} || {0}()", methodBase.Name, TrimText( text, methodBase ) );
		}

		public static void WarnCore( string text, MethodBase methodBase )
		{
			Contract.Requires( text != null );
			Contract.Requires( methodBase != null );
			Contract.Requires( methodBase.Name != null );

			MyCtx.Warn( methodBase.Name, TrimText( text, methodBase ) );
			TraceSource.TraceEvent( TraceEventType.Warning, 0, "{1} || {0}()", methodBase.Name, TrimText( text, methodBase ) );
		}

		private static MethodBase GetMethodBase( int level )
		{
			var stackTrace											= new StackTrace();
			var stackFrame											= stackTrace.GetFrame( level );
			var methodBase											= stackFrame.GetMethod();
			return methodBase;
		}

		//private static string TrimText( MethodBase methodBase )
		//{
		//    return String.Format( "[{0}]", methodBase.DeclaringType.ToString().Replace( BaseNameSpace, "" ) );
		//}

		private static string TrimText( string text, MethodBase methodBase )
		{
			Contract.Requires( text != null );
			Contract.Requires( methodBase != null );

			// ReSharper disable ConditionIsAlwaysTrueOrFalse
			if ( methodBase.DeclaringType == null )
				return "[??] " + text;
			// ReSharper restore ConditionIsAlwaysTrueOrFalse

			return String.Format( "[{0}] {1}", methodBase.DeclaringType.ToString().Replace( BaseNameSpace, "" ), text );
		}
	}
}