using System;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Web;

namespace Veondo.Common
{
	public static class EmailService
	{
		public static void Send( string subject, string body, string from, string to, string replyTo = null, string cc = null, string bcc = null )
		{
			Contract.Requires( !String.IsNullOrEmpty( subject ) );
			Contract.Requires( !String.IsNullOrEmpty( body ) );
			Contract.Requires( !String.IsNullOrEmpty( from ) );
			Contract.Requires( !String.IsNullOrEmpty( to ) );

			if ( !String.IsNullOrEmpty( HttpContext.Current.Request.Url.Host ) && MyCtx.SystemIsTestSystem() )
				from												= (HttpContext.Current.Request.Url.Host + " | " + from);

			Send(
				subject,
				body,
				new MailAddress( from ),
				new MailAddress( to ),
				!String.IsNullOrEmpty( replyTo ) ? new MailAddress( replyTo ) : null,
				!String.IsNullOrEmpty( cc ) ? new MailAddress( cc ) : null,
				!String.IsNullOrEmpty( bcc ) ? new MailAddress( bcc ) : null );
		}

		public static void Send( string subject, string body, MailAddress from, MailAddress to, MailAddress replyTo = null, MailAddress cc = null, MailAddress bcc = null )
		{
			Contract.Requires( !String.IsNullOrEmpty( subject ) );
			Contract.Requires( !String.IsNullOrEmpty( body ) );
			Contract.Requires( from != null );
			Contract.Requires( to != null );

			using ( var message = new MailMessage( from, to ) ) {
				message.IsBodyHtml									= true;
				message.Subject										= subject;
				message.Body										= body;

				if ( bcc != null ) { message.Bcc.Add( bcc ); }
				if ( cc != null ) { message.CC.Add( cc ); }
				if ( replyTo != null ) { message.ReplyToList.Add( replyTo ); }

				using ( var mailClient = new SmtpClient() ) {
					mailClient.Send( message );
				}
			}
		}
	}
}