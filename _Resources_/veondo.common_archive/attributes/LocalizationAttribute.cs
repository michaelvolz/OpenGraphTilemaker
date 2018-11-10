using System.Web.Mvc;
using JetBrains.Annotations;
using Veondo.Common.LanguageSupport;

namespace Veondo.Common.Attributes
{
	public class LocalizationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting( [NotNull] ActionExecutingContext filterContext )
		{
			new SelectedLanguage( filterContext.RouteData, filterContext.HttpContext ).Localize();

			base.OnActionExecuting( filterContext );
		}
	}
}