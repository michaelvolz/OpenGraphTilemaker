using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Veondo.Common.LanguageSupport
{
	public static class SwitchLanguageHelper
	{
		private class Language
		{
			public string Url { get; set; }
			public string ActionName { get; set; }
			public string ControllerName { get; set; }
			public RouteValueDictionary RouteValues { get; set; }
			public bool IsSelected { get; set; }

			[UsedImplicitly]
			public MvcHtmlString HtmlSafeUrl
			{
				get { return MvcHtmlString.Create( Url ); }
			}
		}

		public static MvcHtmlString LanguageSelectorLink( this HtmlHelper htmlHelper, string cultureName, string selectedText, string unselectedText,
			IDictionary<string, object> htmlAttributes, string languageRouteName = "lang", bool strictSelected = false )
		{
			var language											= htmlHelper.LanguageUrl( cultureName, languageRouteName, strictSelected );

			if ( language.IsSelected )
				if ( htmlAttributes == null ) {
					htmlAttributes									= new Dictionary<string, object> { { "class", "selected" } };
				} else {
					htmlAttributes.Add( "class", "selected" );
				}

			if ( IsBlogArea( htmlHelper ) ) {
				return htmlHelper.RouteLink( language.IsSelected ? selectedText : unselectedText, "Blog_default", language.RouteValues, htmlAttributes );
			}

			return htmlHelper.RouteLink( language.IsSelected ? selectedText : unselectedText, "Localization", language.RouteValues, htmlAttributes );
		}

		private static bool IsBlogArea( HtmlHelper htmlHelper )
		{
			return htmlHelper.ViewContext.RequestContext.RouteData.DataTokens["area"] != null && htmlHelper.ViewContext.RequestContext.RouteData.DataTokens["area"].ToString().ToLower() == "Blog".ToLower();
		}

		private static Language LanguageUrl( this HtmlHelper htmlHelper, string cultureName, string languageRouteName = "lang", bool strictSelected = false )
		{
			cultureName												= cultureName.ToLower();
			var routeValues											= new RouteValueDictionary( htmlHelper.ViewContext.RouteData.Values );

			// copy the query strings into the route values to generate the link
			var queryString											= htmlHelper.ViewContext.HttpContext.Request.QueryString;

			foreach ( string key in queryString ) {
				if ( queryString[key] != null && !string.IsNullOrWhiteSpace( key ) ) {
					if ( routeValues.ContainsKey( key ) ) {
						routeValues[key]							= queryString[key];
					} else {
						routeValues.Add( key, queryString[key] );
					}
				}
			}

			var actionName											= routeValues["action"].ToString();
			var controllerName										= routeValues["controller"].ToString();

			routeValues[languageRouteName]							= cultureName;

			// generate the language specify url
			var urlHelper											= new UrlHelper( htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection );

			string url												= urlHelper.RouteUrl( "Localization", routeValues );

			// check whether the current thread ui culture is this language
			var current_lang_name									= Thread.CurrentThread.CurrentUICulture.Name.ToLower();
			var isSelected											= strictSelected ?
																		 current_lang_name == cultureName :
																		 current_lang_name.StartsWith( cultureName );

			return new Language {
				Url													= url,
				ActionName											= actionName,
				ControllerName										= controllerName,
				RouteValues											= routeValues,
				IsSelected											= isSelected
			};
		}
	}
}