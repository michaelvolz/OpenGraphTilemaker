using FluentAssertions;
using Xunit;

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public class DummyTest
    {
        [Fact]
        public void IncrementCounterRequest()
        {
            var condition = true;

            condition.Should().BeTrue();
        }
    }
}