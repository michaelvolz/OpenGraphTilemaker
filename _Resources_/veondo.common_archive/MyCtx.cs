using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Veondo.Common
{
	public class MyCtx
	{
		private const string _warnMessagePrefix						= "## ";
		public static HttpContext Ctx { get { return HttpContext.Current; } }

		public static void RegisterRoutes( RouteCollection routes )
		{
			Contract.Requires( routes != null );

			routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );
			routes.IgnoreRoute( "{*robotstxt}", new { robotstxt=@"(.*/)?robots.txt(/.*)?" } );
			routes.IgnoreRoute( "{*favicon}", new { favicon=@"(.*/)?favicon.ico(/.*)?" } );

			//routes.MapRouteLowercase(
			//    "Root",
			//    "",
			//    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);

			routes.MapRouteLowercase(
				 "Error",
				 "Error/{aspxErrorCode}",
				 new { controller = "Error", action = "Index", aspxErrorCode = UrlParameter.Optional }
			 );

			routes.MapRouteLowercase(
				"Localization",
				"{lang}/{controller}/{action}/{id}",
				new { lang = "en", controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

		public static void FixInvalidUrlCasing()
		{
			Contract.Requires( Ctx != null );

			string baseURL											= ( Ctx.Request.Url.Scheme + "://" 
																		+ Ctx.Request.Url.Authority 
																		+ Ctx.Request.Url.AbsolutePath );

			if ( UrlShouldBeLowerCased( baseURL ) ) {
				baseURL												= baseURL.ToLower() 
																		+ Ctx.Request.Url.Query;
				Redirect( baseURL );
			}
		}

		public static void FixInvalidLoginPage()
		{
			Contract.Requires( Ctx != null );

			if ( IsInvalidLoginPage() ) {
				var url												= Ctx.Request.Url.Scheme + "://"
																	  + Ctx.Request.Url.Authority + "/"
																	  + Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
																	  + Ctx.Request.Url.AbsolutePath.ToLower()
																	  + Ctx.Request.Url.Query;
				Redirect( url );
			}
		}

		private static bool IsInvalidLoginPage()
		{
			Contract.Requires( Ctx != null );

			var path												= Ctx.Request.Url.AbsolutePath;
			return path.ToLower().StartsWith( "/Account/Login".ToLower() );
		}

		private static void Redirect( string baseURL )
		{
			Contract.Requires( Ctx != null );

			Ctx.Response.Clear();
			Ctx.Response.Status										= "301 Moved Permanently";
			Ctx.Response.AddHeader( "Location", baseURL );
			Ctx.Response.End();
		}

		private static bool UrlShouldBeLowerCased( string baseURL )
		{
			Contract.Requires( baseURL != null );

			var lower												= baseURL.ToLower();
			return Regex.IsMatch( baseURL, @"[A-Z]" ) 
			       && !( lower.Contains( "/Scripts/".ToLower() ) )
			       && !( lower.Contains( "/Content/".ToLower() ) )
			       && !( lower.Contains( "/App_Sprites/".ToLower() ) )
			       && !( lower.Contains( "Combres".ToLower() ) ) 
			       && !( lower.Contains( "JavaScript".ToLower() ) ) 
			       && !( lower.Contains( "Authenticate".ToLower() ) ) 
			       && !( lower.Contains( "MetaWeblog".ToLower() ) )
			       && !( lower.Contains( "Glimpse".ToLower() ) ) 
			       && !( lower.Contains( "Elmah".ToLower() )
			);
		}

		[UsedImplicitly]
		public static bool IsIphone()
		{
			Contract.Requires( Ctx != null );

			return Ctx.Request.UserAgent != null 
			       && Ctx.Request.UserAgent.ToLower().Contains( "iphone" );
		}

		public static bool SystemIsDeveloperSystem()
		{
			Contract.Requires( Ctx != null );

			return Ctx.Request.Url.Host == "localhost" 
			       || Ctx.Request.Url.Host == "127.0.0.1"
			       || Ctx.Request.Url.Host == "xps1710"
			       || Ctx.Request.Url.Host == "lightcycle";
		}

		public static bool SystemIsTestSystem()
		{
			Contract.Requires( Ctx != null );

			return Ctx.Request.Url.Host == "localhost"
			       || Ctx.Request.Url.Host == "127.0.0.1"
			       || Ctx.Request.Url.Host == "xps1710"
			       || Ctx.Request.Url.Host == "lightcycle"
			       || Ctx.Request.Url.Host == "alpha.veondo.com"
			       || Ctx.Request.Url.Host == "beta.veondo.com"
			       || Ctx.Request.Url.Host == "veondo-alpha.redmuffin.net"
			       || Ctx.Request.Url.Host == "veondo-beta.redmuffin.net";
		}

		public static bool IsWebCrawler()
		{
			Contract.Requires( Ctx != null );

			var request												= Ctx.Request;

			var isCrawler 											= request.Browser.Crawler;
			if ( !isCrawler ) {
				var regEx 											= new Regex( "Slurp|slurp|ask|Ask|Teoma|teoma|Baidu|baidu" );
				if ( request.UserAgent != null )
					isCrawler 										= regEx.Match( request.UserAgent ).Success;
			}

			if ( isCrawler && Ctx.Request.Url.Host.ToLower() != "veondo.com".ToLower() ) {
				Ctx.Response.RedirectPermanent( "http://veondo.com/" );
				return true;
			}

			return isCrawler;
		}

		public static bool IsTimingValid()
		{
			Contract.Requires( Ctx != null );

			if ( Ctx.Response.ContentType != "text/html" ) { return true; }
			if ( Ctx.Request.Url.AbsoluteUri.Contains( "jqueryFileTree.aspx" ) ) { return true; }
			if ( Ctx.Request.Url.AbsoluteUri.Contains( ".htm" ) ) { return true; }
			if ( !Ctx.IsDebuggingEnabled ) { return true; }
			return false;
		}

		public static void Write( string message )
		{
			Contract.Requires( message != null );

			if ( Ctx != null )
				Ctx.Trace.Write( message );
			else {
				Debug.WriteLine( message );
				Trace.Write( message );
			}
		}

		public static void Write( string category, string message )
		{
			Contract.Requires( message != null );
			Contract.Requires( category != null );

			if ( Ctx != null )
				Ctx.Trace.Write( category, message );
			else {
				Debug.WriteLine( category, message );
				Trace.Write( category, message );
			}
		}

		public static void Write( string category, string message, Exception errorInfo )
		{
			Contract.Requires( message != null );
			Contract.Requires( category != null );
			Contract.Requires( errorInfo != null );

			if ( Ctx != null )
				Ctx.Trace.Write( category, message, errorInfo );
			else {
				Debug.WriteLine( category, message, errorInfo );
				Trace.Write( category, String.Format( "{0}\r\n{1}", message, errorInfo ) );
			}
		}

		public static void Warn( string message )
		{
			Contract.Requires( message != null );

			if ( Ctx != null ) {
				Trace.TraceWarning( message );
			} else {
				Debug.WriteLine( _warnMessagePrefix + message );
			}
		}

		public static void Warn( string category, string message )
		{
			Contract.Requires( message != null );
			Contract.Requires( category != null );

			if ( Ctx != null ) {
				Trace.TraceWarning( String.Format( "{0}, {1}", category, message ) );
			} else {
				Debug.WriteLine( _warnMessagePrefix + category, message );
			}
		}

		public static void Warn( string category, string message, Exception errorInfo )
		{
			Contract.Requires( message != null );
			Contract.Requires( category != null );
			Contract.Requires( errorInfo != null );

			if ( Ctx != null ) {
				Trace.TraceWarning( String.Format( "{0}, {1}\r\n{2}", category, message, errorInfo ) );
			} else {
				Debug.WriteLine( _warnMessagePrefix + category, message, errorInfo );
			}
		}
	}
}
