using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Veondo.Common.Data.Models.Blog;
using Veondo.Common.Data.Models.Business;

namespace Veondo.Common.Data
{
	public class VeondoContextInitializer : DropCreateDatabaseIfModelChanges<VeondoContext>
	{
		protected override void Seed( VeondoContext context )
		{
			SetSqlIndexes( context );

			var flynn = new UserProfile {
				FullName = "Michael Volz",
				FacebookId = "1513806517",
				Emails = new List<Email> {
					new Email {
						Address = "mvolz@redmuffin.de",
						IsVerified = true,
						IsPrimary = true,
					},
				}
			};

			var users = new List<UserProfile> {
				flynn,
			};

			users.ForEach( u => context.UserProfiles.Add( u ) );
			context.SaveChanges();

			var blog = new Tag { Name = "Blog" };
			var d2011 = new Tag { Name = "2011" };
			var d201106 = new Tag { Name = "2011-06" };

			var tags = new List<Tag> {
			    blog,
			    d2011,
			    d201106,
			};

			tags.ForEach( t => context.Tags.Add( t ) );
			context.SaveChanges();

			var blogposts = new List<BlogPost> {
				new BlogPost {
					Title = "H1 The Quick Brown Fox Jumps Over The Lazy Dog",
					Description = @"<h2>H2 The Quick Brown Fox Jumps Over The Lazy Dog</h2>
 
<h3>H3 The Quick Brown Fox Jumps Over The Lazy Dog</h3>
 
<h4>H4 The Quick Brown Fox Jumps Over The Lazy Dog</h4>
 
<h5>H5 The Quick Brown Fox Jumps Over The Lazy Dog</h5>
 
<h6>H6 The Quick Brown Fox Jumps Over The Lazy Dog</h6><p>H7 The Quick Brown Fox Jumps Over The Lazy Dog</p>

<p>Lorem Ipsum is <b>simply dummy text of the printing and typesetting industry. </b>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also<i> the leap into electronic typesetting, remaining essentially unchanged. It w</i>as popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).</p><pre>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of ""de Finibus Bonorum et Malorum"" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, ""Lorem ipsum dolor sit amet.."", comes from a line in section 1.10.32.<br><br></pre><blockquote>The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from ""de Finibus Bonorum et Malorum"" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham. <br><br></blockquote>

<p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p>",
					Author = flynn,
					DateCreated = DateTime.Now,
					Tags = new List<Tag> {
						blog,
					}
				},
				new BlogPost {
					Title = "Ein geklauter Blogeintrag zum rum basteln!",
					Description = @"<p><a href=""http://3.bp.blogspot.com/-OjKoVwovgtM/Tf5pqFZyktI/AAAAAAAAA-8/uExX3kSlwHE/s1600/4ojos1.jpg""><img border=""0"" alt="""" src=""http://3.bp.blogspot.com/-OjKoVwovgtM/Tf5pqFZyktI/AAAAAAAAA-8/uExX3kSlwHE/s400/4ojos1.jpg""></a></p> <p>You do things.<br></p> <p>You try it, this way, that way. You stray, you flop and then you flip again, and something, some things come out of it.</p>  <div style=""padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px"" id=""scid:5737277B-5D6D-4f48-ABFC-DD9C333F4C5D:65f3cf1d-6757-4faa-abdd-f301cd6c541a""><embed height=""252"" type=""application/x-shockwave-flash"" width=""448"" src=""http://www.youtube.com/v/lTx3G6h2xyA?hd=1"" wmode=""transparent""></embed>  <div style=""width: 448px; clear: both; font-size: 0.8em"" class=""wlEditField"" wlpropertypath=""Video.caption"" defaulttext=""Enter video caption here"" maxcharactersaccepted=""245"">Amazing Mashup / 39 Songs</div></div>  <p><br><a href=""http://2.bp.blogspot.com/--94MIeXdtng/Tf5pZwLGGBI/AAAAAAAAA-k/VNoDjhevtys/s1600/4811820893_89f9a5b877_b.jpg""><img border=""0"" alt="""" src=""http://2.bp.blogspot.com/--94MIeXdtng/Tf5pZwLGGBI/AAAAAAAAA-k/VNoDjhevtys/s400/4811820893_89f9a5b877_b.jpg""></a><br>You do them and please, please, you think, do not ask me what I'm doing, what my political take on this, for the moment now I just have a political in-take, the out is not political to my best knowledge. Fortunately, your knowledge is not best. You see, you do things.<br>And although most of them, you can honestly say, you know little about, the matter speaks for you. (Which, of course, does not mean you do not try to talk with it, for it, explain it, relate it and convey it, extrapolate it, and prove where it, the matter, stands).<br><a href=""http://3.bp.blogspot.com/--yrMJAbecdo/Tf5pZopYDrI/AAAAAAAAA-c/LgfBoT_7X7o/s1600/4813646168_5297244847_b.jpg""><img border=""0"" alt="""" src=""http://3.bp.blogspot.com/--yrMJAbecdo/Tf5pZopYDrI/AAAAAAAAA-c/LgfBoT_7X7o/s400/4813646168_5297244847_b.jpg""></a></p> <p>Some of the works you work, frankly, are worthy of the highest criticism. They are, yes it has been said before, the flops. Or worse, they have the wrong ideas, wrong media, wrong impressions and plenty-wrong outcomes.<br><a href=""http://3.bp.blogspot.com/-3ZMZOMR_5vI/Tf5paYfNb7I/AAAAAAAAA-s/ilp93EF-2ow/s1600/4810050195_35f7a93f7b_b.jpg""><img border=""0"" alt="""" src=""http://3.bp.blogspot.com/-3ZMZOMR_5vI/Tf5paYfNb7I/AAAAAAAAA-s/ilp93EF-2ow/s400/4810050195_35f7a93f7b_b.jpg""></a><br>Yet within these plenty-wrong outcomes, things are born. And these things might just make connections, little roots holding on to little pieces of earth. Not that roots hold on to any particular piece, but this metaphor just decided to go its own way, and we at New Art listen to metaphors, so yes, there might be no palpable piece of anything that the roots hold to, yet the work (by now it is work) is starting to appear as if it were actually something, about something, into something, for something. It gains weight.<br><a href=""http://2.bp.blogspot.com/-LpK2lu_N2ZY/Tf5pZQ94O1I/AAAAAAAAA-U/c27ztq1u4ag/s1600/4815460635_db06784b3b_b.jpg""><img border=""0"" alt="""" src=""http://2.bp.blogspot.com/-LpK2lu_N2ZY/Tf5pZQ94O1I/AAAAAAAAA-U/c27ztq1u4ag/s400/4815460635_db06784b3b_b.jpg""></a><br>And then, at some ungiven points, not necessarily at the end or at any sort of finale, the Holy-Flip happens. It could be a form, it could be filled with air or helium, it could be pretty far away from you, but still yours, still stemming from this surprizing head. You might say ""things came into place"", but you have no clue what you are saying, you don't have the perspective, you just enjoy it, the fact that now it seems clear, there is a connection, things are being said which you knew you wanted to say or wanted someone to say, some other head maybe.<br></p>",
					Author = flynn,
					DateCreated = DateTime.Now,
					Tags = new List<Tag> {
						blog,
						d2011,
					}
				}
			};

			blogposts.ForEach( b => context.BlogPosts.Add( b ) );
			context.SaveChanges();

			var artworks = new List<Artwork> {
		        new Artwork {
		            Translations = new List<ArtworkTranslation> {
						new ArtworkTranslation {
							Culture = "de",
							Title = "Feuerhüdrant Supermann",
							Description = "Ein Feuerhüdrant umgebaut als Supermann.\n\rStandort: Düsseldorf.",		
						},
						new ArtworkTranslation {
							Culture = "en",
							Title = "(englischer Text) Feuerhüdrant Supermann",
							Description = "(englischer Text) Ein Feuerhüdrant umgebaut als Supermann.\n\rStandort: Düsseldorf.",		
						}
		            }
		        },
				
		        new Artwork {
					Translations = new List<ArtworkTranslation> {
						new ArtworkTranslation {
							Culture = "de",
							Title = "Laternenpfahl Atomrakte",
							Description = "Umgebauter Laternenpfahl. Stellt nun eine einzelne amerikanische nukleare Atomrakte dar.\n\rEinzelstück.\n\rStandort: London.",
						}
					}
		        },
		    };

			artworks.ForEach( a => context.Artworks.Add( a ) );
			context.SaveChanges();
		}

		private void SetSqlIndexes( DbContext context )
		{
			context.Database.ExecuteSqlCommand( "CREATE UNIQUE INDEX UIX_Emails_Address ON Emails ( Address )" );
		}
	}
}