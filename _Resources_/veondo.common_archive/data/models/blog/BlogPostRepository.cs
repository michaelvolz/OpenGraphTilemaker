using System.Data;

namespace Veondo.Common.Data.Models.Blog
{
	public class BlogPostRepository : RepositoryBase<BlogPost>
	{
		public BlogPostRepository( VeondoContext context ) : base( context ) { }

		public override void InsertOrUpdate( BlogPost blogpost )
		{
			if ( blogpost.BlogPostId == default( int ) ) {
				Add( blogpost );

			} else {
				Context.Entry( blogpost ).State						= EntityState.Modified;
				Context.Entry( blogpost.Author ).State				= EntityState.Modified;

				foreach ( var entity in blogpost.Tags ) {
					Context.Entry( entity ).State					= EntityState.Modified;
				}
			}
		}

	}
}