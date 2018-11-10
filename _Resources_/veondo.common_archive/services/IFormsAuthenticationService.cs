namespace Veondo.Common
{
	public interface IFormsAuthenticationService
	{
		void SignIn( string userName, bool createPersistentCookie );
		void SignOut();
	}
}