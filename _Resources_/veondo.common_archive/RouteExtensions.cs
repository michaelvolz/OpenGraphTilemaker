using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Veondo.Common
{
	public class LowercaseRoute : Route
	{
		public LowercaseRoute( string url, IRouteHandler routeHandler )
			: base( url, routeHandler ) { }

		[UsedImplicitly]
		public LowercaseRoute( string url, RouteValueDictionary defaults, IRouteHandler routeHandler )
			: base( url, defaults, routeHandler ) { }

		[UsedImplicitly]
		public LowercaseRoute( string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler )
			: base( url, defaults, constraints, routeHandler ) { }

		[UsedImplicitly]
		public LowercaseRoute( string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler )
			: base( url, defaults, constraints, dataTokens, routeHandler ) { }

		public override VirtualPathData GetVirtualPath( RequestContext requestContext, RouteValueDictionary values )
		{
			VirtualPathData path									= base.GetVirtualPath( requestContext, values );

			if ( path != null ) {
				var elements										= path.VirtualPath.Split( "?".ToCharArray() );

				path.VirtualPath									= elements[0].ToLowerInvariant();
				if ( elements.Length == 2 )
					path.VirtualPath								= elements[0].ToLowerInvariant() + "?" + elements[1];

				if ( elements.Length > 2 )
					throw new InvalidOperationException( "'element' count can never be greater 2! Invalid url with more than one '?'" );
			}

			return path;
		}
	}

	public static class RouteCollectionExtensions
	{
		[UsedImplicitly]
		public static void MapRouteLowercase( this RouteCollection routes, [CanBeNull] string name, string url, object defaults )
		{
			Contract.Requires( routes != null );
			Contract.Requires( url != null );

			routes.MapRouteLowercase( name, url, defaults, null );
		}

		private static void MapRouteLowercase( this RouteCollection routes, [CanBeNull] string name, string url, object defaults, object constraints )
		{
			Contract.Requires( routes != null );
			Contract.Requires( url != null );

			var route												= new LowercaseRoute( url, new MvcRouteHandler() ) {
				Defaults											= new RouteValueDictionary( defaults ),
				Constraints											= new RouteValueDictionary( constraints )
			};

			if ( String.IsNullOrEmpty( name ) )
				routes.Add( route );
			else
				routes.Add( name, route );
		}
	}
}