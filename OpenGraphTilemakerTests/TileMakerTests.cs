using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using OpenGraphTilemaker;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemakerTests
{
    public class TileMakerTests : BaseTest
    {
        public TileMakerTests(ITestOutputHelper testConsole) : base(testConsole)
        {
        }

        [Fact]
        public async Task ErroneousUrl_CreatesErrorEntry()
        {
            var tileMaker = new TileMaker();
            var httpClient = new HttpClient();

            await tileMaker.ScrapeAsync(httpClient,new Uri("http://brokenurl"), false);

            tileMaker.HtmlMetaTags.Should().BeNull();
            tileMaker.Error.Should().NotBeNull();
        }

        [Fact]
        public async Task LoadWebEnhanced()
        {
            var tileMaker = new TileMaker();
            var httpClient = new HttpClient();
            
            var result = await tileMaker.LoadWebEnhanced(httpClient, new Uri("http://aspnetmonsters.com"));

            result.Should().NotBeNull();
            TestConsole.WriteLine(result.DocumentNode.InnerHtml);
        }

        [Fact]
        public async Task ParseData_AllValuesCorrect()
        {
            var tileMaker = new TileMaker();

            await tileMaker.ScrapeHtml("./TestData/TestHtml1.html");

            tileMaker.HtmlMetaTags.Should().NotBeNullOrEmpty();
            foreach (var node in tileMaker.HtmlMetaTags)
            {
                TestConsole.WriteLine(node.Attributes.Aggregate(
                    "\t", (s, attribute) => s += $"{attribute.Name} = {attribute.Value} ## "));
            }

            var md = tileMaker.OpenGraphMetadata;
            md.Should().NotBeNull();
            md.Type.Should().NotBeNull();
            md.Type.Should().BeEquivalentTo("Article");
            md.Title.Should()
                .BeEquivalentTo("Microsoft launches Spend iOS app that automatically tracks and matches expenses");
            md.Url.Should().BeEquivalentTo("https://9to5mac.com/2018/10/19/microsoft-spend-ios-app/");
            md.Description.Should().BeEquivalentTo(
                "Microsoft is out with a new iOS app that looks to automate keeping tabs on business expenses. Coming from the same team behind the mileage tracking app, MileIQ, Spend is designed to make expense an…");
            md.SiteName.Should().BeEquivalentTo("9to5Mac");
            md.Image.Should()
                .BeEquivalentTo(
                    "https://9to5mac.com/wp-content/uploads/sites/6/2018/10/microsoft-spend-expsense-ios-app.jpg?quality=82&#038;strip=all&#038;w=1600");
            md.ImageWidth.Should().Be(1600);
            md.ImageHeight.Should().Be(900);
            md.Locale.Should().BeEquivalentTo("en_US");
            md.ArticlePublishedTime.Should().Be(DateTime.Parse("2018-10-19T15:39:27+00:00"));
            md.ArticleModifiedTime.Should().Be(DateTime.Parse("2018-10-19T17:22:55+00:00"));
        }

        [Fact]
        public async Task ScrapeHtml()
        {
            var tileMaker = new TileMaker();
            var httpClient = new HttpClient();

            await tileMaker.ScrapeAsync(httpClient, new Uri("https://9to5mac.com/2018/10/19/microsoft-spend-ios-app/"), false);

            tileMaker.HtmlMetaTags.Should().NotBeNull();
        }

        [Fact]
        public async Task ScrapeHtmlAsync()
        {
            var tileMaker = new TileMaker();
            var httpClient = new HttpClient();

            await tileMaker.ScrapeAsync(httpClient,
                new Uri("https://recode.net/2018/11/2/18053424/elon-musk-tesla-spacex-boring-company-self-driving-cars-saudi-twitter-kara-swisher-decode-podcast"), false);

            tileMaker.HtmlMetaTags.Should().NotBeNullOrEmpty();
            foreach (var node in tileMaker.HtmlMetaTags)
            {
                TestConsole.WriteLine(node.Attributes.Aggregate(
                    "\t", (s, attribute) => s += $"{attribute.Name} = {attribute.Value} ## "));
            }

            var md = tileMaker.OpenGraphMetadata;
            md.Should().NotBeNull();
            md.Type.Should().NotBeNull();
            md.Type.Should().BeEquivalentTo("Article");
            md.Title.Should()
                .BeEquivalentTo("Elon Musk: The Recode interview");
            md.Url.Should().BeEquivalentTo("https://www.recode.net/2018/11/2/18053424/elon-musk-tesla-spacex-boring-company-self-driving-cars-saudi-twitter-kara-swisher-decode-podcast");
            md.Description.Should().BeEquivalentTo(
                "Musk talks about his \"excruciating\" 2018, fighting with journalists on Twitter, why Tesla won’t build an electric scooter and much more.");
            md.SiteName.Should().BeEquivalentTo("Recode");
            md.Image.Should()
                .BeEquivalentTo(
                    "https://cdn.vox-cdn.com/thumbor/0VWGPLAhgTPzvzBYJBZOUiJzVtI=/0x215:3000x1786/fit-in/1200x630/cdn.vox-cdn.com/uploads/chorus_asset/file/13372511/REC_Elon_LedeImage__1_.png");
            md.ImageWidth.Should().Be(1200);
            md.ImageHeight.Should().Be(630);
            md.ArticlePublishedTime.Should().Be(DateTime.Parse("2018-11-02T09:04:02+00:00"));
            md.ArticleModifiedTime.Should().Be(DateTime.Parse("2018-11-02T09:04:02+00:00"));
        }
    }
}