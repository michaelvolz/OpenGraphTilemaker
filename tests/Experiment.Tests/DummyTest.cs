using FluentAssertions;
using Xunit;

namespace Experiment.Tests
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