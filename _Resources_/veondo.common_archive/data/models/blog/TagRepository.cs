using System.Data;

namespace Veondo.Common.Data.Models.Blog
{
	public class TagRepository : RepositoryBase<Tag>
    {
		public TagRepository( VeondoContext context ) : base( context ) { }

		public override void InsertOrUpdate( Tag tag )
        {
			if ( tag.TagId == default( int ) ) {
				Add( tag );
            
			} else {
				Context.Entry( tag ).State							= EntityState.Modified;
            }
        }
    }
}