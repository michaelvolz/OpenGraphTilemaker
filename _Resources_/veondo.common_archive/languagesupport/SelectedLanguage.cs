using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;

namespace Veondo.Common.LanguageSupport
{
	public class SelectedLanguage
	{
		private readonly HttpContextBase _httpContext;
		private readonly string[] _userLanguages;

		private const string RouteLanguageValue						= "lang";
		private const string DefaultLanguage						= "en";
		private const string CookieName								= "Veondo.CurrentUICulture";

		private readonly object _routeLanguageValue;

		private string _language;
		private readonly RouteData _routeData;

		public string Language
		{
			get { return _language; }
		}

		public SelectedLanguage( RouteData routeData, HttpContextBase httpContext )
		{
			_httpContext											= httpContext;
			_routeData												= routeData;
			_routeLanguageValue										= routeData.Values[RouteLanguageValue];
			_userLanguages											= httpContext.Request.UserLanguages;
			_language												= IdentifyLanguage();
		}


		public void Localize()
		{
			CheckLanguage();
			UpdateCurrentUICulture();
			UpdateRouteDataValues();
			SaveCookieLanguage();
		}

		private void CheckLanguage()
		{
			_language = _language.Substring(0, 2);

			if ( _language.ToLower() != "de" && _language.ToLower() != "en" )
			{
				_language											= DefaultLanguage.ToLower();
			}
		}

		private void UpdateCurrentUICulture()
		{
			Thread.CurrentThread.CurrentUICulture					= CultureInfo.CreateSpecificCulture( _language );
		}

		private void UpdateRouteDataValues()
		{
			_routeData.Values[RouteLanguageValue]					= _language;
		}

		private void SaveCookieLanguage()
		{
			try {
				var cookie											= new HttpCookie( CookieName, _language ) {
					Expires											= DateTime.Now.AddYears( 1 )
				};

				_httpContext.Response.SetCookie( cookie );
			} catch { }
		}

		private string IdentifyLanguage()
		{
			if ( LanguageValueExists() && LangugeValueIsValidAndKnown( _routeLanguageValue ) ) {
				Trace.Write( "Language found in RouteValue: '" + _routeLanguageValue +"'" );
				return Verified( _routeLanguageValue.ToString() );
			}

			var cookie												= _httpContext.Request.Cookies[CookieName];
			if ( cookie != null ) {
				Trace.Write( "Language found in Cookie: '" + cookie.Value +"'" );
				return Verified( cookie.Value );
			}

			// TODO: foreach browserlanguages to see if one is supported!
			var language											= _userLanguages != null ? _userLanguages[0] : DefaultLanguage;

			Trace.Write( "Language found in BrowserLanguage: '" + language +"'" );
			return Verified( language );
		}

		private bool LanguageValueExists()
		{
			return _routeLanguageValue != null && !string.IsNullOrWhiteSpace( _routeLanguageValue.ToString() );
		}

		private bool LangugeValueIsValid( string language )
		{
			return language.Length == 2 || ( language.Length == 5 && language.Contains( "-" ) );
		}

		private string Verified( string language )
		{
			return LangugeValueIsValidAndKnown( language ) ? language.ToLower() : DefaultLanguage.ToLower();
		}

		private bool LangugeValueIsValidAndKnown( object language )
		{
			if ( language != null ) {
				return LangugeValueIsValidAndKnown( language.ToString() );
			}

			return false;
		}

		private bool LangugeValueIsValidAndKnown( string language )
		{
			var languages											= new List<String> { "en", "de" };
			language												= language.ToLower();
			var isValidAndKnown										= ( LangugeValueIsValid( language ) && languages.Contains( language ) );

			Trace.WriteIf( !isValidAndKnown, "language: " + language + ", Valid=" + isValidAndKnown );

			//if ( !isValidAndKnown ) {
			//    throw new InvalidOperationException( "Unsupported language code '" + language + "'!\r\n Supported language codes: " + languages.Aggregate( "", ( current, l ) => current + l + ", " ).Trim().Trim( ",".ToCharArray() ) );
			//}

			return true;
		}
	}
}