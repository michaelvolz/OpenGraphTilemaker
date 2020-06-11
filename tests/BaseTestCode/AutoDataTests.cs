using System.Diagnostics.CodeAnalysis;
using AutoFixture.Xunit2;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace BaseTestCode
{
    /// <summary>
    ///     AutoFixture CheatSheet https://github.com/AutoFixture/AutoFixture/wiki/Cheat-Sheet.
    /// </summary>
    public class AutoDataTests : BaseTest<AutoDataTests>
    {
        public AutoDataTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Theory, AutoData]
        public void IntroductoryTest(int expectedNumber, SomeTestClass sut)
        {
            var result = SomeTestClass.Echo(expectedNumber);

            result.Should().Be(expectedNumber);
            sut.AnotherClass.Should().NotBeNull();
            sut.AnotherClass!.Currency.Should().BeGreaterThan(0);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test")]
        public class SomeTestClass
        {
            public string? Text { get; set; }
            public int Number { get; set; }
            public AnotherClass? AnotherClass { get; [UsedImplicitly] set; }

            public static int Echo(int expectedNumber) => expectedNumber;
        }

        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test")]
        public class AnotherClass
        {
            public decimal Currency { get; [UsedImplicitly] set; }
        }
    }
}