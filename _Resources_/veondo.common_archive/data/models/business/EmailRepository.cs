using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.Security.Application;

namespace Veondo.Common.Data.Models.Business
{
	public class EmailRepository : RepositoryBase<Email>
	{
		public EmailRepository( VeondoContext context ) : base( context ) { }

		public void RegisterForNewsletter( string email )
		{
			email													= SanitizeEmail( email );

			var existingEmail										= FindBy( x => x.Address == email ).SingleOrDefault();

			if ( existingEmail == null ) {
				InsertOrUpdate( NewNewsletterEmail( email ) );

			} else {
				if ( !IsUsersEmail() )
					return;

				existingEmail.GetNewsletter							= true;
				InsertOrUpdate( existingEmail );
			}
		}

		private static string SanitizeEmail( string email )
		{
			email													= Sanitize( email );
			Debug.Assert( email.Contains( "@" ) );

			return email;
		}

		private static string Sanitize( string value )
		{
			if ( String.IsNullOrWhiteSpace( value ) )
				throw new ArgumentException( "'value' can not be null or whitespace!" );

			return Sanitizer.GetSafeHtmlFragment( value );
		}

		private bool IsUsersEmail()
		{
			// TODO: nur für eigene E-Mail-Adressen!!!
			return false;
		}

		private static Email NewNewsletterEmail( string email )
		{
			return new Email {
				Address												= email,
				IsPrimary											= false,
				IsVerified											= false,
				GetNewsletter										= true,
			};
		}

		public override void InsertOrUpdate( Email email )
		{
			email.Address											= SanitizeEmail( email.Address );

			if ( email.EmailId == default( int ) ) {
				Add( email );

			} else {
				Context.Entry( email ).State						= EntityState.Modified;
			}
		}
	}
}