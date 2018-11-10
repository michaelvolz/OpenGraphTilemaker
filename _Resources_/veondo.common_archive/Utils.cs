using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using JetBrains.Annotations;

namespace Veondo.Common
{
	/// <summary>
	/// Summary description for Utils
	/// HttpContext.Current.IsDebuggingEnabled
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Checks for ip address.
		/// </summary>
		/// <param name="addressToCheck">The address to check.</param>
		/// <returns></returns>
		[UsedImplicitly]
		private static bool CheckForIpAddress( [NotNull] string addressToCheck )
		{
			Contract.Requires( !String.IsNullOrEmpty( addressToCheck ) );

			var ipAddresses											= Dns.GetHostEntry( Dns.GetHostName() ).AddressList;

			if ( ipAddresses == null )
				throw new InvalidOperationException( "IP Adresses not found!" );

			return ipAddresses.Where( t => t.ToString() == addressToCheck ).Any();
		}

		public static void LogerrorToFile( [NotNull] string pathAndFilename, [NotNull] string stackTrace, [NotNull] string message, [NotNull] string url, int lineNumber )
		{
			Contract.Requires( !String.IsNullOrEmpty( pathAndFilename ) );
			Contract.Requires( stackTrace != null );
			Contract.Requires( message != null );
			Contract.Requires( url != null );

			const string Line										= " --------------- ";

			var name												= Path.GetFileName( pathAndFilename );
			if ( String.IsNullOrEmpty( name ) )
				throw new InvalidOperationException( "Filename not found! " +  pathAndFilename );

			var file												= DateTime.Now.ToString( "yyyy-MM-dd__" ) + name;
			pathAndFilename											= pathAndFilename.Replace( name, file );
			var allLines 											= new[] { Line +  DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) + Line, "## Url: " + url, "## Line: " + lineNumber, "## Message: " + message, "## Stack: " + stackTrace };

			Contract.Assume( !String.IsNullOrEmpty( pathAndFilename ) );
			FileRemoveWriteProtection( pathAndFilename );

			if ( File.Exists( pathAndFilename ) )
				File.AppendAllLines( pathAndFilename, allLines );
			else
				File.WriteAllLines( pathAndFilename, allLines );
		}

		public static void FileRemoveWriteProtection( [NotNull] string filename )
		{
			Contract.Requires( !String.IsNullOrEmpty( filename ) );

			if ( !File.Exists( filename ) )
				return;

			var file 												= new FileInfo( filename );
			if ( file.IsReadOnly )
				file.Attributes 									-= FileAttributes.ReadOnly;
		}

	}
}