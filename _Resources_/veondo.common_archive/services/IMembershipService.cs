using System.Web.Security;

namespace Veondo.Common
{
	public interface IMembershipService
	{
		int MinPasswordLength { get; }
		bool ValidateUser( string userName, string password );
		MembershipCreateStatus CreateUser( string userName, string password, string email, string providerUserKey );
		bool ChangePassword( string userName, string oldPassword, string newPassword );
		MembershipUser GetUser( string providerUserKey );
	}
}