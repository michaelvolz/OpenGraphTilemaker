using System.ComponentModel.DataAnnotations;

namespace Veondo.Common.Data.Models.Business
{
	public class Email
	{
		[Key]
		public int EmailId { get; set; }

		[Required, DataAnnotationsExtensions.Email, MaxLength(256)]
		public string Address { get; set; }

		public bool IsPrimary { get; set; }

		public bool IsVerified { get; set; }

		public bool GetNewsletter { get; set; }

		public virtual UserProfile User { get; set; }
	}
}