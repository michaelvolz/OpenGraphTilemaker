using AutoFixture.Xunit2;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BaseTestCode
{
    /// <summary>
    ///     AutoFixture CheatSheet https://github.com/AutoFixture/AutoFixture/wiki/Cheat-Sheet.
    /// </summary>
    public class AutoDataTests : BaseTest<AutoDataTests>
    {
        public AutoDataTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Theory]
        [AutoData]
        public void IntroductoryTest(int expectedNumber, SomeTestClass sut)
        {
            var result = SomeTestClass.Echo(expectedNumber);

            result.Should().Be(expectedNumber);
            sut.AnotherClass.Should().NotBeNull();
            sut.AnotherClass!.Currency.Should().BeGreaterThan(0);
        }

        [UsedImplicitly]
        public class SomeTestClass
        {
            [UsedImplicitly] public string? Text { get; set; }
            [UsedImplicitly] public int Number { get; set; }
            public AnotherClass? AnotherClass { get; set; }

            public static int Echo(int expectedNumber) => expectedNumber;
        }

        [UsedImplicitly]
        public class AnotherClass
        {
            public decimal Currency { get; set; }
        }
    }
}