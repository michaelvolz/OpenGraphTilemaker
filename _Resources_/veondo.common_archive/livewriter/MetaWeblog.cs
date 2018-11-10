using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;
using JetBrains.Annotations;
using Veondo.Common.Data;
using Veondo.Common.Data.Models.Blog;
using Veondo.Common.Data.Models.Business;

namespace Veondo.Common.LiveWriter
{
	[UsedImplicitly]
	public class MetaWeblog : XmlRpcService, IMetaWeblog
	{
		private readonly VeondoContext _context;
		private readonly string _host;

		public MetaWeblog()
		{
			_context 												= new VeondoContext();
			_host													= HttpContext.Current.Request.Url.Host;
		}

		string IMetaWeblog.AddPost( string blogid, string username, string password, Post post, bool publish )
		{
			ValidateUser( username, password );

			var blogPostRepository									= new BlogPostRepository( _context );
			var userProfileRepository								= new UserProfileRepository( _context );
			var blogPost											= new BlogPost();

			blogPost.Title											= post.title;
			blogPost.Description									= post.description;
			blogPost.DateCreated									= post.dateCreated > default(DateTime) ? post.dateCreated : DateTime.Now;

			if ( post.permalink != null ) {
				blogPost.PermaLink									= post.permalink;
			}

			int userid;
			int.TryParse( post.userid, out userid );
			blogPost.Author											= userProfileRepository.Find( userid > 0 ? userid : 1 );

			if ( post.categories != null ) {
				blogPost.Tags										= ExtractTags( post.categories );
			}

			blogPostRepository.Add( blogPost );
			blogPostRepository.Save();

			return blogPost.BlogPostId.ToString();
		}

		private static string[] ExtractCategories( ICollection<Tag> tags )
		{
			if ( tags == null ) {
				return new string[0];
			}

			var list												= new string[tags.Count];
			var i = 0;
			foreach ( var tag in tags ) {
				list[i++]											= tag.Name;
			}

			return list;
		}

		private ICollection<Tag> ExtractTags( IEnumerable<string> tagNames )
		{
			if ( tagNames == null ) {
				return new List<Tag>();
			}

			var tagRepository										= new TagRepository( _context );

			return tagNames.Select(
				tagName => tagRepository.FindBy( tag => tag.Name == tagName ).SingleOrDefault()
			).Where( tag => tag != null ).ToList();
		}

		bool IMetaWeblog.UpdatePost( string postid, string username, string password, Post post, bool publish )
		{
			ValidateUser( username, password );

			var blogPostRepository									= new BlogPostRepository( _context );
			var userProfileRepository								= new UserProfileRepository( _context );
			var pid													= Convert.ToInt32( postid );
			var blogPost											= blogPostRepository.FindAllIncluding( x => x.Author, x => x.Tags )
																		.SingleOrDefault( y => y.BlogPostId == pid );

			if ( blogPost == null )
				return false;

			Debug.Assert( blogPost.Author != null );

			blogPost.Title											= post.title;
			blogPost.Description									= post.description;
			blogPost.DateCreated									= post.dateCreated > default( DateTime ) ? post.dateCreated : DateTime.Now;

			if ( post.permalink != null ) {
				blogPost.PermaLink									= post.permalink;
			}

			int userid;
			int.TryParse( post.userid, out userid );
			blogPost.Author											= userProfileRepository.Find( userid > 0 ? userid : 1 );

			if ( post.categories != null ) {
				blogPost.Tags										= ExtractTags( post.categories );
			}

			blogPostRepository.InsertOrUpdate( blogPost );
			blogPostRepository.Save();

			return true;
		}

		Post IMetaWeblog.GetPost( string postid, string username, string password )
		{
			ValidateUser( username, password );

			var blogPostRepository									= new BlogPostRepository( _context );
			var pid													= Convert.ToInt32( postid );
			var blogPost											= blogPostRepository.FindAllIncluding( b => b.Author, b => b.Tags )
																		.Where( x => x.BlogPostId == pid ).SingleOrDefault();

			if ( blogPost == null )
				throw new XmlRpcFaultException( 2041, "Post not found" );

			var post = new Post {
				title												= blogPost.Title,
				permalink											= string.Format( "http://{0}/en/blog/blogposts/details/{1}", _host, blogPost.BlogPostId ),
				description											= blogPost.Description,
				dateCreated											= blogPost.DateCreated,
				postid												= blogPost.BlogPostId,
				userid												= blogPost.Author.UserProfileId.ToString(),
				categories											= ExtractCategories( blogPost.Tags ),
			};

			return post;
		}

		CategoryInfo[] IMetaWeblog.GetCategories( string blogid, string username, string password )
		{
			ValidateUser( username, password );

			var tagRepository										= new TagRepository( _context );
			var categories											= new CategoryInfo[tagRepository.Count];

			var tags												= tagRepository.FindAll().ToList();
			for ( int index = 0; index < tags.Count; index++ ) {
				var tag												= tags[index];
				var category										= new CategoryInfo {
					description										= tag.Name,
					title											= tag.Name,
					categoryid										= tag.TagId.ToString(),
					htmlUrl											= string.Format( "http://{0}/en/blog/tags/details/{1}", _host, tag.TagId ),
					rssUrl											= string.Format( "http://{0}/en/blog/tags/details/{1}", _host, tag.TagId ),
				};

				categories[index]									= category;
			}

			return categories;
		}

		Post[] IMetaWeblog.GetRecentPosts( string blogid, string username, string password, int numberOfPosts )
		{
			ValidateUser( username, password );

			var blogPostRepository									= new BlogPostRepository( _context );
			var blogPost											= new BlogPost();
			var host												= HttpContext.Current.Request.Url.Host;

			var finalPosts											= new Post[blogPostRepository.Count];
			var posts												= blogPostRepository.FindAllIncluding( b => b.Author, b => b.Tags ).ToList();

			for ( int index = 0; index < posts.Count; index++ ) {
				var post											= posts[index];
				var finalPost										= new Post {
					title											= post.Title,
					permalink										= string.Format( "http://{0}/en/blog/blogposts/details/{1}", host, blogPost.BlogPostId ),
					description										= post.Description,
					dateCreated										= post.DateCreated,
					postid											= post.BlogPostId,
					categories										= ExtractCategories( post.Tags ),
					userid											= post.Author.UserProfileId.ToString()
				};

				finalPosts[index]									= finalPost;
			}

			return finalPosts;
		}

		MediaObjectInfo IMetaWeblog.NewMediaObject( string blogid, string username, string password, MediaObject mediaObject )
		{
			ValidateUser( username, password );

			var name												= mediaObject.name;
			var type												= mediaObject.type;
			var media												= mediaObject.bits;

			const string virtualPath								= "/Content/Blog/";
			var path												= HttpContext.Current.Server.MapPath( string.Format( "~{0}", virtualPath ) );

			var names												= name.Split( @"/".ToCharArray() );
			var filename											= names[names.Length-1];

			var stream												= File.Create( path + filename );
			stream.Write( media, 0, media.Length );
			stream.Flush();
			stream.Close();
			stream.Dispose();

			var objectInfo											= new MediaObjectInfo { url = GetFullUrl( virtualPath + filename ) };

			return objectInfo;
		}

		bool IMetaWeblog.DeletePost( string key, string postid, string username, string password, bool publish )
		{
			ValidateUser( username, password );

			var blogPostRepository									= new BlogPostRepository( _context );
			blogPostRepository.Delete( Convert.ToInt32( postid ) );
			blogPostRepository.Save();

			return true;
		}

		BlogInfo[] IMetaWeblog.GetUsersBlogs( string key, string username, string password )
		{
			ValidateUser( username, password );

			var blogs												= new BlogInfo[1];
			blogs[0] = new BlogInfo {
				blogName                							= "Veondo(ALPHA) - Blog",
				blogid												= "1",
				url													= string.Format( "http://{0}/en/blog/", _host ),
			};

			return blogs;

			// TODO: Implement your own logic
			// The blogid should be the same as the blogID from the RSD. Live Writer assumes that when the user adds the blog account, they will 
			// use the url to the actual blog they are adding and not to a more generic account. 
			// http://social.msdn.microsoft.com/Forums/en-US/wlwdev/thread/120e9562-1d02-485d-89a9-03364609e87c
			//return sBlogs.Values.ToArray();
		}

		UserInfo IMetaWeblog.GetUserInfo( string key, string username, string password )
		{
			ValidateUser( username, password );

			var user = new UserInfo() {
				email												= "email",
				firstname											= "firstname",
				lastname											= "lastname",
				nickname											= "nickname",
				url													= "url",
				userid												= "1",
			};

			return user;
		}

		private void ValidateUser( string username, string password )
		{
			if ( !IsUserValid( username, password ) )
				throw new XmlRpcFaultException( 2041, "User is not valid!" );
		}

		private bool IsUserValid( string username, string password )
		{
			// TODO: Implement the logic to validate the user
			return true;
		}

		private static string GetFullUrl( params string[] paths )
		{
			return string.Format( "http://{0}{1}", HttpContext.Current.Request.Url.Authority, string.Join( "/", paths ) );
		}

		private static string GetFullUrl( /*params string[] paths*/ int blogId )
		{
			return string.Format( "http://{0}/en/blog/blogposts/details/{1}", HttpContext.Current.Request.Url.Authority, blogId );
		}
	}
}
