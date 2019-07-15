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

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            condition.Should().BeTrue();
        }
    }
}