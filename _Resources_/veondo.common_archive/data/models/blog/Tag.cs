using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Veondo.Common.Data.Models.Blog
{
	public class Tag
	{
		[Key]
		public int TagId { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<BlogPost> BlogPosts { get; set; }
	}
}