using System.Data;

namespace Veondo.Common.Data.Models.Business
{
	public class UserProfileRepository : RepositoryBase<UserProfile>
	{
		public UserProfileRepository( VeondoContext context ) : base( context ) { }

		public override void InsertOrUpdate( UserProfile userprofile )
		{
			if ( userprofile.UserProfileId == default( int ) ) {
				Add( userprofile );

			} else {
				Context.Entry( userprofile ).State					= EntityState.Modified;

				if ( userprofile.Emails != null )
					foreach ( var entity in userprofile.Emails ) {
						Context.Entry( entity ).State				= EntityState.Modified;
					}
			}
		}
	}
}