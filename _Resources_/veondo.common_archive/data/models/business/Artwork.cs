using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Veondo.Common.Data.Models.Business
{
	public class Artwork
	{
		[Key]
		public int ArtworkID { get; set; }

		[DataType( DataType.Text ), FileExtensions( "png|jpg|jpeg|gif" )]
		public string ImageName { get; set; }

		public virtual ICollection<ArtworkTranslation> Translations { get; set; }
	}
}