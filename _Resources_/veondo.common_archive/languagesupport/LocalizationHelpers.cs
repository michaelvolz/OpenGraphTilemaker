using System;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace Veondo.Common.LanguageSupport
{
	public static class LocalizationHelpers
	{
		public static IHtmlString MetaAcceptLanguage<T>( this HtmlHelper<T> html )
		{
			var acceptLanguage = HttpUtility.HtmlAttributeEncode( Thread.CurrentThread.CurrentUICulture.ToString() );
			return new HtmlString( String.Format( "<meta name=\"accept-language\" content=\"{0}\" />", acceptLanguage ) );
		}
	}
}