using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using Elmah;
using MvcMiniProfiler;

namespace Veondo.Common
{
	public class TimingModule : IHttpModule
	{
		private const string WarmupDone								= "WARMUPDONE";
		private const int PageRenderTimeout							= 4;

		public void Dispose() { }

		public void Init( HttpApplication context )
		{
			context.PreSendRequestHeaders							+= OnPreSendRequestHeaders;
			context.BeginRequest									+= OnBeginRequest;
			context.EndRequest										+= OnEndRequest;
			context.PreSendRequestHeaders							+= OnPreSendRequestHeaders;
		}

		/// <summary>
		/// Event raised just before ASP.NET sends HTTP headers to the client.
		/// </summary>
		/// <param name="sender">Event source.</param>
		/// <param name="e">Event arguments.</param>
		void OnPreSendRequestHeaders( object sender, EventArgs e )
		{
			RemoveHeaders();
		}

		private static void RemoveHeaders()
		{
			if ( HttpContext.Current == null || HttpContext.Current.Request.IsLocal )
				return;

			NameValueCollection headers								= HttpContext.Current.Response.Headers;

			headers.Remove( "Server" );
			headers.Remove( "ETag" );
			headers.Remove( "X-Powered-By" );
			headers.Remove( "X-AspNet-Version" );
			headers.Remove( "X-AspNetMvc-Version" );
		}

		void OnBeginRequest( object sender, EventArgs e )
		{
			if ( MyCtx.IsWebCrawler() ) { return; }

			if ( MyCtx.IsTimingValid() ) { return; }

			if ( HttpContext.Current != null && MyCtx.SystemIsDeveloperSystem() ) {
				if ( MiniProfiler.Current != null )
					MiniProfiler.Start();
			}

			Trace.Write( "BeginRequest", String.Empty );

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			HttpContext.Current.Items["Stopwatch"] = stopwatch;
		}

		void OnEndRequest( object sender, EventArgs e )
		{
			if ( MyCtx.IsWebCrawler() ) { return; }

			if ( MyCtx.IsTimingValid() ) { return; }

			Trace.Write( "EndRequest", String.Empty );

			var response											= HttpContext.Current.Response;
			var stopwatch											= ( Stopwatch )HttpContext.Current.Items["Stopwatch"];
			var elapsedTime											= "#error#";
			var ts													= new TimeSpan( 0 );

			if ( stopwatch != null ) {
				stopwatch.Stop();

				ts													= stopwatch.Elapsed;
				elapsedTime											= String.Format( "{0}s", new DateTime( ts.Ticks ).ToString( "s.fff" ) );
			}

			response.Write( "<!-- Debug Infos-->\r\n<b class=timer> " + elapsedTime + "</b> " );

			if ( MiniProfiler.Current != null )
				MiniProfiler.Stop();

			SendErrorIfPageLoadWasHigh( elapsedTime, ts );
		}

		private static void SendErrorIfPageLoadWasHigh( string elapsedTime, TimeSpan ts )
		{
			if ( HttpContext.Current == null || HttpContext.Current.Request.IsLocal )
				return;

			var request												= HttpContext.Current.Request;
			var url													= request.Url;
			var requestMethod										= request.ServerVariables["REQUEST_METHOD"];
			var host												= request.ServerVariables["HTTP_HOST"];
			var useragent											= request.UserAgent;
			var controller											= request.RequestContext.RouteData.Values["controller"];
			var action												= request.RequestContext.RouteData.Values["action"];

			var warmupDone											= HttpContext.Current.Application[WarmupDone] as string;
			if ( warmupDone != null && warmupDone == "true" && ts > TimeSpan.FromSeconds( PageRenderTimeout ) ) {
				ErrorSignal
					.FromCurrentContext()
					.Raise( new TimeoutException(
							String.Format(
								"The time to render the page was greater than {7} seconds!\r\nHost: '{0}'\r\nUrl: '{1}'\r\nRequestMethod: '{2}'\r\nUserAgent: '{3}'\r\nController: '{4}'\r\nAction: '{5}'\r\nRendertime: '{6}'",
								host, url, requestMethod, useragent, controller, action, elapsedTime, PageRenderTimeout ) ) );
			}
		}
	}
}