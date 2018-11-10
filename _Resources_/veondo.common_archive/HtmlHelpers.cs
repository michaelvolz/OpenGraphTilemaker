using System;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Veondo.Common.LanguageSupport;
using System.Web;

namespace Veondo.Common
{
	public static class HtmlHelpers
	{
		//public static DbConnection GetOpenConnection()
		//{
		//    //var conn = ProfiledDbConnection.Get( new SqlCeConnection( @"data source=|DataDirectory|\Veondo.Web.Models.VeondoContext.sdf" ) );
		//    //return ObjectContextUtils.CreateObjectContext<VeondoContext>(conn);

		//    //var cnn = new SqlCeConnection( @"data source=|DataDirectory|\Veondo.Web.Models.VeondoContext.sdf" );

		//    //// wrap the connection with a profiling connection that tracks timings 
		//    //return MvcMiniProfiler.Data.ProfiledDbConnection.Get( cnn, MiniProfiler.Current );
		//}

		public static string GetQuery( string u )
		{
			// 1
			// Try to match start of query with "&q=". These matches are ideal.
			int start = u.IndexOf( "&q=", StringComparison.Ordinal );
			int length = 3;
			// 2
			// Try to match part with q=. This may be prefixed by another letter.
			if ( start == -1 ) {
				start = u.IndexOf( "q=", StringComparison.Ordinal );
				length = 2;
			}
			// 3
			// Try to match start of query with "p=".
			if ( start == -1 ) {
				start = u.IndexOf( "p=", StringComparison.Ordinal );
				length = 2;
			}
			// 4
			// Return if not possible
			if ( start == -1 ) {
				return string.Empty;
			}
			// 5
			// Advance N characters
			start += length;
			// 6
			// Find first & after that
			int end = u.IndexOf( '&', start );
			// 7
			// Use end index if no & was found
			if ( end == -1 ) {
				end = u.Length;
			}
			// 8
			// Get substring between two parameters
			string sub = u.Substring( start, end - start );
			// 9
			// Get the decoded URL
			string result = HttpUtility.UrlDecode( sub );
			// 10
			// Get the HTML representation
			result = HttpUtility.HtmlEncode( result );
			// 11
			// Prepend sitesearch label to output
			if ( u.IndexOf( "sitesearch", StringComparison.Ordinal ) != -1 ) {
				result = "sitesearch: " + result;
			}
			return result;
		}

		public static MvcHtmlString CanonicalUrlLinkTag( this HtmlHelper htmlhelper )
		{
			return new MvcHtmlString( LinkTag( ConstructUrl( htmlhelper.ViewContext.RouteData, htmlhelper.ViewContext.RequestContext.HttpContext ) ) );
		}

		public static MvcHtmlString CanonicalUrl( this HtmlHelper htmlhelper )
		{
			return new MvcHtmlString( ConstructUrl( htmlhelper.ViewContext.RouteData, htmlhelper.ViewContext.RequestContext.HttpContext ) );
		}

		private static string ConstructUrl( RouteData routeData, HttpContextBase httpContextBase )
		{
			var language											= new SelectedLanguage( routeData, httpContextBase ).Language;
			var rawUrl												= httpContextBase.Request.Url;

			string path;

			if ( rawUrl.AbsolutePath.Contains( language ) ) {
				path												= String.Format( "{0}://{1}{2}", rawUrl.Scheme, rawUrl.Host, rawUrl.AbsolutePath );
			} else {
				path												= String.Format( "{0}://{1}/{2}{3}", rawUrl.Scheme, rawUrl.Host, language, rawUrl.AbsolutePath );
			}

			return SanitizeUrl( path );
		}

		private static string SanitizeUrl( string path )
		{
			path													= path.ToLower();

			if ( path.Count( c => c == '/' ) > 3 ) {
				path												= path.TrimEnd( '/' );
			}

			if ( path.EndsWith( "/index" ) ) {
				path												= path.Substring( 0, path.Length - 6 );
			}

			return path;
		}

		private static string LinkTag( string path )
		{
			var linkTag												= new TagBuilder( "link" );
			linkTag.MergeAttribute( "rel", "canonical");
			linkTag.MergeAttribute( "href", path );

			return linkTag.ToString( TagRenderMode.SelfClosing );
		}
	}
}