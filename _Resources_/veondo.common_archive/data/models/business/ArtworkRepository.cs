using System.Data;

namespace Veondo.Common.Data.Models.Business
{
	public class ArtworkRepository : RepositoryBase<Artwork>
	{
		public override void InsertOrUpdate( Artwork artwork )
		{
			if ( artwork.ArtworkID == default( int ) ) {
				Add( artwork );

			} else {
				Context.Entry( artwork ).State						= EntityState.Modified;

				foreach ( var entity in artwork.Translations ) {
					Context.Entry( entity ).State					= EntityState.Modified;
				}
			}
		}
	}
}