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
        private const string DummyText = "Earthly of he parasites at so and for call shrine of old pomp to could that fondly one did hight Earthly";

        [Fact]
        public async Task TagCloud_InsertText_ReturnsValidCloud() {
            var tagCloud = new Common.TagCloud.TagCloud();

            await tagCloud.InsertAsync(DummyText, DummyText, DummyText);

            var cloud = tagCloud.Cloud;

            cloud.Should().NotBeNull();
            cloud.Should().ContainKey("Earthly".ToLowerInvariant());
            cloud.Should().NotContainKey("of".ToLowerInvariant());
            cloud.Should().NotContainKey("he".ToLowerInvariant());
            cloud.Should().ContainKey("parasites".ToLowerInvariant());
            cloud.Should().NotContainKey("at".ToLowerInvariant());
            cloud.Should().NotContainKey("so".ToLowerInvariant());
            cloud.Should().NotContainKey("and".ToLowerInvariant());
            cloud.Should().NotContainKey("for".ToLowerInvariant());
            cloud.Should().ContainKey("call".ToLowerInvariant());
            cloud.Should().ContainKey("shrine".ToLowerInvariant());
            cloud.Should().NotContainKey("of".ToLowerInvariant());
            cloud.Should().NotContainKey("old".ToLowerInvariant());
            cloud.Should().ContainKey("pomp".ToLowerInvariant());
            cloud.Should().NotContainKey("old".ToLowerInvariant());
            cloud.Should().NotContainKey("could".ToLowerInvariant());
            cloud.Should().NotContainKey("that".ToLowerInvariant());
            cloud.Should().ContainKey("fondly".ToLowerInvariant());
            cloud.Should().NotContainKey("one".ToLowerInvariant());
            cloud.Should().NotContainKey("did".ToLowerInvariant());
            cloud.Should().ContainKey("hight".ToLowerInvariant());

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
            cloud.Should().ContainKey("Earthly".ToLowerInvariant());
            cloud.Should().ContainKey("hight".ToLowerInvariant());

            var (key, value) = cloud.First();
            key.Should().BeEquivalentTo("earthly");
            value.Should().Be(2);

            TestConsole.WriteLine("{@cloud}", cloud);
        }
    }
}
