using System.IO;
using System.Web;
using MarkdownSharp;
using System.Web.Mvc;

namespace Veondo.Common
{
	/// <summary>
	/// Helper class for transforming Markdown.
	/// </summary>
	public static class MarkdownHelper
	{
		/// <summary>
		/// An instance of the Markdown class that performs the transformations.
		/// </summary>
		static readonly Markdown markdownTransformer = new Markdown();

		/// <summary>
		/// Transforms a string of Markdown into HTML.
		/// </summary>
		/// <param name="helper">HtmlHelper - Not used, but required to make this an extension method.</param>
		/// <param name="text">The Markdown that should be transformed.</param>
		/// <returns>The HTML representation of the supplied Markdown.</returns>
		public static IHtmlString Markdown( this HtmlHelper helper, string text )
		{
			// Transform the supplied text (Markdown) into HTML.
			string html = markdownTransformer.Transform( text );

			// Wrap the html in an MvcHtmlString otherwise it'll be HtmlEncoded and displayed to the user as HTML :(
			return new MvcHtmlString( html );
		}

		public static IHtmlString Markdown( this HtmlHelper helper, FileInfo fileInfo )
		{
			var file												= File.ReadAllText( fileInfo.FullName );

			// Transform the supplied text (Markdown) into HTML.
			string html = markdownTransformer.Transform( file );

			// Wrap the html in an MvcHtmlString otherwise it'll be HtmlEncoded and displayed to the user as HTML :(
			return new MvcHtmlString( html );
		}
	}
}