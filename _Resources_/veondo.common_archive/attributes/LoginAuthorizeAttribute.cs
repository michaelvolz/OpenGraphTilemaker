using System.Web.Mvc;
using JetBrains.Annotations;

namespace Veondo.Common.Attributes
{
	[UsedImplicitly]
	public sealed class LoginAuthorizeAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization( AuthorizationContext filterContext )
		{
			bool skipAuthorization = ( !filterContext.ActionDescriptor.IsDefined( typeof( AuthorizeAttribute ), true )
				&& !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined( typeof( AuthorizeAttribute ), true ) )
				&& (
					filterContext.ActionDescriptor.IsDefined( typeof( AllowAnonymousAttribute ), true )
					|| filterContext.ActionDescriptor.ControllerDescriptor.IsDefined( typeof( AllowAnonymousAttribute ), true )
				);

			if ( !skipAuthorization ) {
				base.OnAuthorization( filterContext );
			}
		}
	}
}