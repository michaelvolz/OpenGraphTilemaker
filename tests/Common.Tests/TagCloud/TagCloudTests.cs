using System.Linq;
using System.Threading.Tasks;
using BaseTestCode;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests.TagCloud
{
    public class TagCloudTests : BaseTest<TagCloudTests>
    {
        public TagCloudTests(ITestOutputHelper testConsole) : base(testConsole) { }
        private const string DummyText = "Earthly, of he. parasites! at? so and; for call asp.net of old pomp!!!!!!!! to could that fondly one did hight Earthly .net " + "“Texas new book is out:";

        [Fact]
        public async Task TagCloud_InsertText_ReturnsValidCloud() {
            var tagCloud = new Common.TagCloud.TagCloud();

            await tagCloud.InsertAsync(DummyText, DummyText, DummyText);

            var cloud = tagCloud.Cloud;

            cloud.Should().NotBeNull();
            cloud.Should().ContainKey("Earthly".ToUpperInvariant());
            cloud.Should().NotContainKey("of".ToUpperInvariant());
            cloud.Should().NotContainKey("he".ToUpperInvariant());
            cloud.Should().ContainKey("parasites".ToUpperInvariant());
            cloud.Should().NotContainKey("at".ToUpperInvariant());
            cloud.Should().NotContainKey("so".ToUpperInvariant());
            cloud.Should().NotContainKey("and".ToUpperInvariant());
            cloud.Should().NotContainKey("for".ToUpperInvariant());
            cloud.Should().ContainKey("call".ToUpperInvariant());
            cloud.Should().ContainKey("asp.net".ToUpperInvariant());
            cloud.Should().NotContainKey("of".ToUpperInvariant());
            cloud.Should().NotContainKey("old".ToUpperInvariant());
            cloud.Should().ContainKey("pomp".ToUpperInvariant());
            cloud.Should().NotContainKey("old".ToUpperInvariant());
            cloud.Should().NotContainKey("could".ToUpperInvariant());
            cloud.Should().NotContainKey("that".ToUpperInvariant());
            cloud.Should().ContainKey("fondly".ToUpperInvariant());
            cloud.Should().NotContainKey("one".ToUpperInvariant());
            cloud.Should().NotContainKey("did".ToUpperInvariant());
            cloud.Should().ContainKey("hight".ToUpperInvariant());
            cloud.Should().ContainKey(".net".ToUpperInvariant());
            cloud.Should().ContainKey("Texas".ToUpperInvariant());
            cloud.Should().NotContainKey("“Texas".ToUpperInvariant());

            var (key, value) = cloud.First();
            key.Should().BeEquivalentTo("earthly");
            value.Should().Be(1);

            TestConsole.WriteLine("{@cloud}", cloud);
        }

        [Fact]
        public async Task TagCloud_InsertTextTwice_ReturnsValidCloud() {
            var tagCloud = new Common.TagCloud.TagCloud();

            await tagCloud.InsertAsync(DummyText);
            await tagCloud.InsertAsync(DummyText);

            var cloud = tagCloud.Cloud;

            cloud.Should().NotBeNull();
            cloud.Should().ContainKey("Earthly".ToUpperInvariant());
            cloud.Should().ContainKey("hight".ToUpperInvariant());

            var (key, value) = cloud.First();
            key.Should().BeEquivalentTo("earthly");
            value.Should().Be(2);

            TestConsole.WriteLine("{@cloud}", cloud);
        }
    }
}
