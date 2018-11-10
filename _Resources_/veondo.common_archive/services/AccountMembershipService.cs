using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Veondo.Common
{
	public class AccountMembershipService : IMembershipService
	{
		private readonly MembershipProvider _provider;

		public AccountMembershipService() : this( null ) { }

		public AccountMembershipService( MembershipProvider provider )
		{
			_provider												= provider ?? Membership.Provider;
		}

		public int MinPasswordLength
		{
			get
			{
				return _provider.MinRequiredPasswordLength;
			}
		}

		public bool ValidateUser( string userName, string password )
		{
			if ( String.IsNullOrEmpty( userName ) )
				throw new ArgumentException( "Value cannot be null or empty.", "userName" );
			if ( String.IsNullOrEmpty( password ) )
				throw new ArgumentException( "Value cannot be null or empty.", "password" );

			return _provider.ValidateUser( userName, password );
		}

		public object StringToGUID( string value )
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create();
			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash( Encoding.Default.GetBytes( value ) );
			return new Guid( data );
		}

		public MembershipCreateStatus CreateUser( string userName, string password, string email, string providerUserKey )
		{
			if ( String.IsNullOrEmpty( userName ) )
				throw new ArgumentException( "Value cannot be null or empty.", "userName" );
			if ( String.IsNullOrEmpty( password ) )
				throw new ArgumentException( "Value cannot be null or empty.", "password" );
			if ( String.IsNullOrEmpty( email ) )
				throw new ArgumentException( "Value cannot be null or empty.", "email" );

			MembershipCreateStatus status;
			if ( !String.IsNullOrWhiteSpace( providerUserKey ) ) {
				object stringToGuid									= StringToGUID( providerUserKey );
				_provider.CreateUser( userName, password, email, null, null, true, stringToGuid, out status );
			} else {
				_provider.CreateUser( userName, password, email, null, null, true, null, out status );
			}

			return status;
		}

		public MembershipUser GetUser( string providerUserKey )
		{
			object stringToGuid									= StringToGUID( providerUserKey );
			return _provider.GetUser( stringToGuid, true );
		}

		public bool ChangePassword( string userName, string oldPassword, string newPassword )
		{
			if ( String.IsNullOrEmpty( userName ) )
				throw new ArgumentException( "Value cannot be null or empty.", "userName" );
			if ( String.IsNullOrEmpty( oldPassword ) )
				throw new ArgumentException( "Value cannot be null or empty.", "oldPassword" );
			if ( String.IsNullOrEmpty( newPassword ) )
				throw new ArgumentException( "Value cannot be null or empty.", "newPassword" );

			// The underlying ChangePassword() will throw an exception rather
			// than return false in certain failure scenarios.
			try {
				MembershipUser currentUser = _provider.GetUser( userName, true /* userIsOnline */);
				return currentUser.ChangePassword( oldPassword, newPassword );
			} catch ( ArgumentException ) {
				return false;
			} catch ( MembershipPasswordException ) {
				return false;
			}
		}

		public MembershipCreateStatus CreateUser( string userName, string password, string email )
		{
			throw new NotImplementedException();
		}
	}
}