using System.Web;
using System.Web.Routing;
using JetBrains.Annotations;
using System.Web.Mvc;

namespace Veondo.Common.Attributes
{
	public sealed class TeaserRedirectAttribute : ActionFilterAttribute
	{
		//TODO: Für Mehrsprachigkeit den richtigen Language Wert ermitteln!
		private const string RedirectUrl							= "/de/Home/Teaser";
		private const string DefaultLanguageValue					= "de";

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			// Only redirect on the first request in a session
			if ( !HttpContext.Current.Session.IsNewSession )
				base.OnActionExecuting( filterContext );

			if ( IsTeaserTime() ) {
				var redirectionRouteValues							= GetRedirectionRouteValues( filterContext.RequestContext );
				filterContext.Result								= new RedirectToRouteResult( redirectionRouteValues );
			}
		}

		// Override this method if you want to customize the controller/action/parameters to which
		// mobile users would be redirected. This lets you redirect users to the mobile equivalent
		// of whatever resource they originally requested.
		private RouteValueDictionary GetRedirectionRouteValues( [UsedImplicitly] RequestContext requestContext )
		{
			return new RouteValueDictionary( new { area = "", controller = "Home", action = "Teaser", lang = CalculateLanguageValue() } );
		}

		private static string CalculateLanguageValue()
		{
			//TODO: Für Mehrsprachigkeit den richtigen Language Wert ermitteln!
			return DefaultLanguageValue;
		}

		private static bool IsTeaserTime()
		{
			return TeaserUtils.ShouldShowOnlyTeaser() && IsIncorrectUrl() && IsNoSpecialPage();
		}

		private static bool IsNoSpecialPage()
		{
			return !HttpContext.Current.Request.Url.PathAndQuery.ToLower().StartsWith( "/Error".ToLower() ) 
				&& !HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains( "Elmah".ToLower() )
				&& !HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains( "JavaScript".ToLower() );
		}

		private static bool IsIncorrectUrl()
		{
			return HttpContext.Current.Request.Url.PathAndQuery.ToLower() != RedirectUrl.ToLower();
		}
	}
}