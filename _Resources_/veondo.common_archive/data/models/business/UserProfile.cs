using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Veondo.Common.Data.Models.Blog;

namespace Veondo.Common.Data.Models.Business
{
	public class UserProfile
	{
		[Key]
		public int UserProfileId { get; set; }

		[Required]
		public string FullName { get; set; }

		public string FacebookId { get; set; }

		public string OpenId { get; set; }

		public virtual ICollection<Email> Emails { get; set; }

		public virtual ICollection<BlogPost> BlogPosts { get; set; }
	}
}