using System;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using CookComputing.XmlRpc;
using JetBrains.Annotations;

namespace Veondo.Common.LiveWriter
{
	[ServiceContract]
	public partial interface IMetaWeblog
	{
		#region MetaWeblog API

		[XmlRpcMethod( "metaWeblog.newPost" ), OperationContract( Action = "metaWeblog.newPost" ), UsedImplicitly]
		string AddPost( string blogid, string username, string password, Post post, bool publish );

		[XmlRpcMethod( "metaWeblog.editPost" ), UsedImplicitly]
		bool UpdatePost( string postid, string username, string password, Post post, bool publish );

		[XmlRpcMethod( "metaWeblog.getPost" ), UsedImplicitly]
		Post GetPost( string postid, string username, string password );

		[XmlRpcMethod( "metaWeblog.getCategories" ), UsedImplicitly]
		CategoryInfo[] GetCategories( string blogid, string username, string password );

		[XmlRpcMethod( "metaWeblog.getRecentPosts" ), UsedImplicitly]
		Post[] GetRecentPosts( string blogid, string username, string password, int numberOfPosts );

		[XmlRpcMethod( "metaWeblog.newMediaObject" ), UsedImplicitly]
		MediaObjectInfo NewMediaObject( string blogid, string username, string password, MediaObject mediaObject );

		#endregion

		#region Blogger API

		[XmlRpcMethod( "blogger.deletePost" ), UsedImplicitly]
		[return: XmlRpcReturnValue( Description = "Returns true." )]
		bool DeletePost( string key, string postid, string username, string password, bool publish );

		[XmlRpcMethod( "blogger.getUsersBlogs" ), UsedImplicitly]
		BlogInfo[] GetUsersBlogs( string key, string username, string password );

		[XmlRpcMethod( "blogger.getUserInfo" ), UsedImplicitly]
		UserInfo GetUserInfo( string key, string username, string password );

		#endregion
	}

	#region IMetaWeblog contract binding
	[ContractClass( typeof( IMetaWeblogContract ) )]
	public partial interface IMetaWeblog { }

	[ContractClassFor( typeof( IMetaWeblog ) )]
	abstract class IMetaWeblogContract : IMetaWeblog
	{
		public string AddPost( string blogid, string username, string password, Post post, bool publish )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public bool UpdatePost( string postid, string username, string password, Post post, bool publish )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public Post GetPost( string postid, string username, string password )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public CategoryInfo[] GetCategories( string blogid, string username, string password )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public Post[] GetRecentPosts( string blogid, string username, string password, int numberOfPosts )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public MediaObjectInfo NewMediaObject( string blogid, string username, string password, MediaObject mediaObject )
		{
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public bool DeletePost( string key, string postid, string username, string password, bool publish )
		{
			Contract.Requires<ArgumentNullException>( !String.IsNullOrEmpty( key ) );
			Contract.Requires<ArgumentNullException>( !String.IsNullOrEmpty( postid ) );
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public BlogInfo[] GetUsersBlogs( string key, string username, string password )
		{
			Contract.Requires<ArgumentNullException>( !String.IsNullOrEmpty( key ) );
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}

		public UserInfo GetUserInfo( string key, string username, string password )
		{
			Contract.Requires<ArgumentNullException>( !String.IsNullOrEmpty( key ) );
			Contract.Requires<ArgumentNullException>( username != null );
			Contract.Requires<ArgumentNullException>( password != null );
			throw new NotImplementedException();
		}
	}
	#endregion
}