using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace BaseTestCode
{
    /// <summary>
    ///     AutoFixture CheatSheet https://github.com/AutoFixture/AutoFixture/wiki/Cheat-Sheet.
    /// </summary>
    public class AutoDataTests : BaseTest
    {
        public AutoDataTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Theory]
        [AutoData]
        public void IntroductoryTest(int expectedNumber, Peter sut) {
            var result = Peter.Echo(expectedNumber);

            result.Should().Be(expectedNumber);
            sut.AnotherClass.Should().NotBeNull();
            sut.AnotherClass.Currency.Should().BeGreaterThan(0);
        }

        public class Peter
        {
            public string Text { get; set; }
            public int Number { get; set; }
            public AnotherClass AnotherClass { get; set; }

            public static int Echo(int expectedNumber) {
                return expectedNumber;
            }
        }

        public class AnotherClass
        {
            public decimal Currency { get; set; }
        }
    }
}
