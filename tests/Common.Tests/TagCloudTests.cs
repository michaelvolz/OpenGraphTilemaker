using System.Linq;
using System.Threading.Tasks;
using BaseTestCode;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests
{
    public class TagCloudTestsTests : BaseTest<TagCloudTestsTests>
    {
        public TagCloudTestsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task TagCloud_InsertText_ReturnsValidCloud() {
            var tagCloud = new TagCloud();

            var text = "Earthly of he parasites at so and for call shrine of old pomp to could that fondly one did hight Earthly";
            await tagCloud.InsertAsync(text);

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
            cloud.Should().ContainKey("shrine".ToUpperInvariant());
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

            var (key, value) = cloud.First();
            key.Should().BeEquivalentTo("earthly");
            value.Should().Be(2);

            TestConsole.WriteLine("{@cloud}", cloud);
        }
    }
}
