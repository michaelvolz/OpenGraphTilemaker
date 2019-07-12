using FluentAssertions;
using Xunit;

namespace Experiment.Web.Client.Tests
{
    public class DummyTest
    {
        [Fact]
        public void IncrementCounterRequest() => true.Should().BeTrue();
    }
}