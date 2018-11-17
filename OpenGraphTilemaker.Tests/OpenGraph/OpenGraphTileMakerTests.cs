using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.OpenGraph;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo
// ReSharper disable ArgumentsStyleLiteral

namespace OpenGraphTilemaker.Tests.OpenGraph
{
    public class OpenGraphTileMakerTests : BaseTest
    {
        public OpenGraphTileMakerTests(ITestOutputHelper testConsole) : base(testConsole) {
            // Arrange
            var options = Options.Create(new DiscCacheOptions {CacheFolder = @"C:\WINDOWS\Temp\"});
            _webLoader = new HttpLoader(new DiscCache(options), CacheState.Disabled);
            _tileMaker = new OpenGraphTileMaker();
            _httpClient = new HttpClient();
        }

        private readonly OpenGraphTileMaker _tileMaker;
        private readonly HttpClient _httpClient;
        private readonly HttpLoader _webLoader;

        [Fact]
        public async Task ErroneousUrl_CreatesErrorEntry() {
            var uri = new Uri("http://brokenurl");

            // Act
            await _tileMaker.ScrapeAsync("source-name", async () => await _webLoader.LoadAsync(_httpClient, uri));

            // Assert
            _tileMaker.Error.Should().NotBeNull();
            _tileMaker.HtmlMetaTags.Should().BeNull();
        }

        [Fact]
        public async Task LoadWeb() {
            // Act
            var result = await _webLoader.LoadAsync(_httpClient,
                new Uri("https://www.hanselman.com/blog/AzureDevOpsContinuousBuildDeployTestWithASPNETCore22PreviewInOneHour.aspx"));

            result.Should().NotBeNull();
            TestConsole.WriteLine(result.DocumentNode.InnerHtml);
        }

        [Fact]
        public async Task ParseData_AllValuesCorrect() {
            // Act
            var source = "./TestData/TestHtml1.html";
            await _tileMaker.ScrapeAsync(source, async () => await FileLoader.LoadAsync(source));

            // Assert
            _tileMaker.Error.Should().BeNull();
            _tileMaker.HtmlMetaTags.Should().NotBeNullOrEmpty();
            foreach (var node in _tileMaker.HtmlMetaTags) {
                TestConsole.WriteLine(node.Attributes.Aggregate(
                    "\t", (s, attribute) => s + $"{attribute.Name} = {attribute.Value} ## "));
            }

            var md = _tileMaker.OpenGraphMetadata;
            md.Should().NotBeNull();
            md.Type.Should().NotBeNull();
            md.Type.Should().BeEquivalentTo("Article");
            md.Title.Should()
                .BeEquivalentTo("Microsoft launches Spend iOS app that automatically tracks and matches expenses");
            md.Url.Should().BeEquivalentTo("https://9to5mac.com/2018/10/19/microsoft-spend-ios-app/");
            md.Description.Should().BeEquivalentTo(
                "Microsoft is out with a new iOS app that looks to automate keeping tabs on business expenses. Coming from the same team behind the mileage tracking app, MileIQ, Spend is designed to make expense an�");
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
        public async Task Scrape() {
            var uri = new Uri("https://www.hanselman.com/blog/AzureDevOpsContinuousBuildDeployTestWithASPNETCore22PreviewInOneHour.aspx");

            // Act
            await _tileMaker.ScrapeAsync("source-name", async () => await _webLoader.LoadAsync(_httpClient, uri));

            // Assert
            _tileMaker.Error.Should().BeNull();
            _tileMaker.HtmlMetaTags.Should().NotBeNull();
        }

        [Fact]
        public async Task ScrapeAsync() {
            var uri = new Uri(
                "https://recode.net/2018/11/2/18053424/elon-musk-tesla-spacex-boring-company-self-driving-cars-saudi-twitter-kara-swisher-decode-podcast");

            // Act
            await _tileMaker.ScrapeAsync("source-name", async () => await _webLoader.LoadAsync(_httpClient, uri));

            // Assert
            _tileMaker.Error.Should().BeNull();
            _tileMaker.HtmlMetaTags.Should().NotBeNullOrEmpty();
            foreach (var node in _tileMaker.HtmlMetaTags) {
                TestConsole.WriteLine(node.Attributes.Aggregate(
                    "\t", (s, attribute) => s + $"{attribute.Name} = {attribute.Value} ## "));
            }

            var md = _tileMaker.OpenGraphMetadata;
            md.Should().NotBeNull();
            md.Type.Should().NotBeNull();
            md.Type.Should().BeEquivalentTo("Article");
            md.Title.Should()
                .BeEquivalentTo("Elon Musk: The Recode interview");
            md.Url.Should().BeEquivalentTo(
                "https://www.recode.net/2018/11/2/18053424/elon-musk-tesla-spacex-boring-company-self-driving-cars-saudi-twitter-kara-swisher-decode-podcast");
            md.Description.Should().BeEquivalentTo(
                "Musk talks about his \"excruciating\" 2018, fighting with journalists on Twitter, why Tesla won�t build an electric scooter and much more.");
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