using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using Veondo.Common.Data.Models.Business;

namespace Veondo.Common.Data.Models.Blog
{
	public class BlogPost
	{
		public BlogPost()
		{
			DateCreated												= DateTime.Now;
		}
		
		[Key]
		public int BlogPostId { get; set; }

		[Required, DataType( DataType.Text )]
		public string Title { get; set; }
	
		[AllowHtml, DataType( DataType.MultilineText )]
		public string Description { get; set; }
		
		public string PermaLink { get; set; }
		
		public string Slug { get; set; }

		public virtual UserProfile Author { get; set; }

		public virtual ICollection<Tag> Tags { get; set; }

		//[Required, DataType( DataType.Date ), DisplayFormat( DataFormatString = "{0:d}", ApplyFormatInEditMode = true )]
		[Required, Date]
		[DisplayFormat( ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}" )]
		public DateTime DateCreated { get; set; }
	}
}