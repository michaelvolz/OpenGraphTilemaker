using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace Veondo.Common
{
	public static class EmailAdresses
	{
		public static string ErrorMailsFrom
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsFrom" );
			}
		}
		public static string ErrorMailsReplyTo
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsReplyTo" );
			}
		}
		public static string ErrorMailsReplyToName
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsReplyTo_Name" );
			}
		}
		public static string ErrorMailsTo
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsTo" );
			}
		}
		public static string ErrorMailsToName
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsTo_Name" );
			}
		}
		public static string ErrorMailsCC
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsCC" );
			}
		}
		public static string ErrorMailsCCName
		{
			get
			{
				Contract.Ensures( Contract.Result<string>() != null );

				return GetConfigValue( "ErrorMailsCC_Name" );
			}
		}

		[UsedImplicitly]
		public static string KontaktMailsFrom { get { return GetConfigValue( "KontaktMailsFrom" ); } }
		[UsedImplicitly]
		public static string KontaktMailsReplyTo { get { return GetConfigValue( "KontaktMailsReplyTo" ); } }
		[UsedImplicitly]
		public static string KontaktMailsReplyToName { get { return GetConfigValue( "KontaktMailsReplyTo_Name" ); } }
		[UsedImplicitly]
		public static string KontaktMailsBCC { get { return GetConfigValue( "KontaktMailsBCC" ); } }
		[UsedImplicitly]
		public static string KontaktMailsBCCName { get { return GetConfigValue( "KontaktMailsBCC_Name" ); } }

		private static string GetConfigValue( string key )
		{
			Contract.Requires( !String.IsNullOrEmpty( key ) );
			Contract.Ensures( Contract.Result<string>() != null );

			var value												= ConfigurationManager.AppSettings[key];
			if ( value == null )
				throw new InvalidOperationException( string.Format( "The key '{0}' in web.config (AppSettings) must not be null!", key ) );

			return value;
		}
	}
}