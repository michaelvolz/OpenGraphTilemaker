using System.Security.Cryptography;
using System.Text;

namespace Veondo.Common.Security
{
	public class Crypto
	{
		public static byte[] Encrypt( string messageString )
		{
			var myRsa												= new RSACryptoServiceProvider( 1024, new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore } );

			var messageBytes										= Encoding.Unicode.GetBytes( messageString );
			var encryptedMessage									= myRsa.Encrypt( messageBytes, false );

			return encryptedMessage;
		}

		public static string Decrypt( byte[] encryptedMessageBytes )
		{
			var myRsa												= new RSACryptoServiceProvider( 1024, new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore } );

			var decryptedBytes										= myRsa.Decrypt( encryptedMessageBytes, false );

			return Encoding.Unicode.GetString( decryptedBytes );
		}
	}
}