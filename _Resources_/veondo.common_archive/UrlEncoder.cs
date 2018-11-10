using System.Text;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace Veondo.Common
{
	public static class UrlEncoder
	{

		// http://www.dominicpettifer.co.uk/Blog/34/asp-net-mvc-and-clean-seo-friendly-urls

		// routes.MapRoute( 
		//     "ViewProduct", 
		//     "products/{id}/{productName}", 
		//     new { controller = "Product", action = "Detail", id = "", productName = "" } 
		// );

		// public ActionResult Detail( int? id, string productName )
		// {
		//     Product product = IProductRepository.Fetch( id );
		//     return View( product );
		// }

		// <%= Url.ToFriendlyUrl(item.Title) %>

		// <a href="<%= Url.Action("View", "Blog", 
		// 	new { id = item.BlogId, blogTitle = Url.ToFriendlyUrl(item.Title) }) %>"> 
		// 	<%= Html.Encode(item.Title) %> 
		// </a>

		// public ActionResult Details( int? id, string productTitle )
		// {
		//    Product product = ProductRepository.Fetch( id );

		//    string realTitle = UrlEncoder.ToFriendlyUrl( product.Title );
		//    string urlTitle = ( productTitle ?? "" ).Trim().ToLower();

		//    if ( realTitle != urlTitle ) {
		//        Response.Status = "301 Moved Permanently";
		//        Response.StatusCode = 301;
		//        Response.AddHeader( "Location", "/Products/" + product.Id + "/" + realTitle );
		//        Response.End();
		//    }

		//    return View( product );
		// }

		[UsedImplicitly]
		public static string ToFriendlyUrl( this UrlHelper helper, string urlToEncode )
		{
			urlToEncode												= ( urlToEncode ?? "" ).Trim().ToLower();
			var url													= new StringBuilder();

			foreach ( char ch in urlToEncode ) {
				switch ( ch ) {
					case ' ':
						url.Append( '-' );
						break;

					case '&':
						url.Append( "and" );
						break;

					case '\'':
						break;

					default:
						if ( ( ch >= '0' && ch <= '9' ) || 
                        ( ch >= 'a' && ch <= 'z' ) ) {
							url.Append( ch );
						} else {
							url.Append( '-' );
						}
						break;
				}
			}

			return url.ToString();
		}
	}
}