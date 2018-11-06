using System;
using System.Linq;
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
        public void ErroneousUrl_CreatesErrorEntry()
        {
            var tileMaker = new TileMaker();

            tileMaker.ScrapeHtml(new Uri("http://brokenurl.de"));

            tileMaker.HtmlMetaTags.Should().BeNull();
            tileMaker.Error.Should().NotBeNull();
        }

        [Fact]
        public void ParseData_AllValuesCorrect()
        {
            var tileMaker = new TileMaker();

            tileMaker.ScrapeHtml("./TestData/TestHtml1.html");

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
            md.Title.Should().BeEquivalentTo("Microsoft launches Spend iOS app that automatically tracks and matches expenses");
            md.Url.Should().BeEquivalentTo("https://9to5mac.com/2018/10/19/microsoft-spend-ios-app/");
            md.Description.Should().BeEquivalentTo("Microsoft is out with a new iOS app that looks to automate keeping tabs on business expenses. Coming from the same team behind the mileage tracking app, MileIQ, Spend is designed to make expense an…");
            md.SiteName.Should().BeEquivalentTo("9to5Mac");
            md.Image.Should().BeEquivalentTo("https://9to5mac.com/wp-content/uploads/sites/6/2018/10/microsoft-spend-expsense-ios-app.jpg?quality=82&#038;strip=all&#038;w=1600");
            md.ImageWidth.Should().Be(1600);
            md.ImageHeight.Should().Be(900);
            md.Locale.Should().BeEquivalentTo("en_US");
            md.ArticlePublishedTime.Should().Be(DateTime.Parse("2018-10-19T15:39:27+00:00"));
            md.ArticleModifiedTime.Should().Be(DateTime.Parse("2018-10-19T17:22:55+00:00"));
        }
    }
}