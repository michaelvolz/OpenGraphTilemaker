using System.Data;

namespace Veondo.Common.Data.Models.Business
{
	public class ArtworkTranslationRepository : RepositoryBase<ArtworkTranslation>
	{
		public override void InsertOrUpdate( ArtworkTranslation artworktranslation )
		{
			if ( artworktranslation.ArtworkTranslationID == default( int ) ) {
				Add( artworktranslation );

			} else {
				Context.Entry( artworktranslation ).State				= EntityState.Modified;
			}
		}
	}
}