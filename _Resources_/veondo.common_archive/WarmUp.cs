using System;
using System.Net;
using System.Threading;
using System.Web;

namespace Veondo.Common
{
	public class PreWarmUp
	{
		private string _host										= String.Empty;
		private const string WarmupDone								= "WARMUPDONE";

		public void RefreshPagesForStartup( HttpApplication applicationInstance )
		{
			var language											= Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

			_host													= string.Format( "{0}{1}/", applicationInstance.Context.Request.Url, language );

			var x = new[] { "Blog/Tags", "Home/ErrorTests", "UserProfiles", "Blog/BlogPosts", "Blog/Blog/" };

			foreach ( var s in x ) {
				Fetch( s );
			}

			applicationInstance.Application[WarmupDone]				= "true";
		}

		private void Fetch( string url )
		{
			WebClient webClient										= null;
			try {
				webClient											= new WebClient();

				webClient.Headers.Add( "user-agent", "Mozilla/4.0 (compatible; Veondo WarmUp Module;)" );
				var deleteme										= webClient.DownloadString( new Uri( ( _host + url ).ToLower() ) );

			} finally {
				if ( webClient != null )
					webClient.Dispose();
			}

		}
	}
}