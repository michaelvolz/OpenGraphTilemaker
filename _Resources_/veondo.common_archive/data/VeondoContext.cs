using System.Data.Entity;
using JetBrains.Annotations;
using Veondo.Common.Data.Models.Blog;
using Veondo.Common.Data.Models.Business;

namespace Veondo.Common.Data
{
	public class VeondoContext : EfDataContextBase
	{
		//private VeondoContext( string connectionString ) : base( connectionString ) { }

		//public static VeondoContext Get()
		//{
		//    var dbConnection										= ProfiledDbConnection.Get( new SqlCeConnection( @"data source=|DataDirectory|\Veondo.Web.Models.VeondoContext.sdf" ) );
		//    var veondoContext										= new VeondoContext( dbConnection.ConnectionString );

		//    return veondoContext;
		//}

		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, add the following
		// code to the Application_Start method in your Global.asax file.
		// Note: this will destroy and re-create your database with every model change.
		// 
		// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Veondo.Web.Models.VeondoWebContext>());

		public DbSet<Artwork> Artworks { get; set; }
		[UsedImplicitly]
		public DbSet<ArtworkTranslation> ArtworkTranslations { get; set; }

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }
		[UsedImplicitly]
		public DbSet<Email> Emails { get; set; }
	}
}