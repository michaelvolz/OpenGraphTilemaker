using System.ComponentModel.DataAnnotations;
using Veondo.Globalization;

namespace Veondo.Common.Data.Models.Business
{
	public class ArtworkTranslation
	{
		public int ArtworkTranslationID { get; set; }

		[MinLength( 2 ), MaxLength( 5 )]
		[Display( Name = "ArtworkTranslation_Culture", ResourceType = typeof( Model ) )]
		public string Culture { get; set; }

		[Required, DataType( DataType.Text ),]
		[Display( Name = "ArtworkTranslation_Title", ResourceType = typeof( Model ) )]
		public string Title { get; set; }

		private string _description;

		[DataType( DataType.MultilineText )]
		[Display( Name = "ArtworkTranslation_Description", ResourceType = typeof( Model ) )]
		public string Description
		{
			get
			{
				if ( _description != null )
					_description = _description.Trim( "\r\n".ToCharArray() );

				return _description;
			}
			set { _description = value.Trim( "\r\n".ToCharArray() ); }
		}

		public virtual Artwork Artwork { get; set; }
	}
}