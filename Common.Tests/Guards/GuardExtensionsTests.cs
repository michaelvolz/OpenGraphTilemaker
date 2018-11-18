using System;
using Ardalis.GuardClauses;
using BaseTestCode;
using Common.Extensions;
using FluentAssertions;
using OpenGraphTilemaker.OpenGraph;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests.Guards
{
    public class GuardExtensionsTests : BaseTest
    {
        public GuardExtensionsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Theory]
        [InlineData(null, "Value cannot be null.")]
        [InlineData("", "Value cannot be empty.")]
        [InlineData("     ", "Value cannot be whitespace.")]
        public void GuardNullOrWhiteSpace(string value, string expected)
        {
            var parameterName = "Jauser";

            try {
                Guard.Against.NullOrWhiteSpace(value, parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain(expected);
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNullOrWhiteSpace)}*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void GuardCondition()
        {
            var parameterName = "Jauser";

            try {
                Guard.Against.Condition(() => DateTime.Now != DateTime.MinValue, parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Condition");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardCondition)}()*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void GuardAssert()
        {
            var parameterName = "Jauser";

            try {
                Guard.Against.Assert(() => DateTime.Now == DateTime.MinValue, parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Assertion");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardAssert)}()*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void GuardEnum()
        {
            var parameterName = "Jauser";

            var invalidValue = 3;
            try {
                Guard.Against.Enum(invalidValue, typeof(CacheState), parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain($"The value of argument '{parameterName}' ({invalidValue}) is invalid for Enum type '{nameof(CacheState)}'.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardEnum)}()*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }


        [Fact]
        public void GuardException_ToString() {
            var parameterName = "Jauser";

            try {
                Guard.Against.Default(new TimeSpan(), parameterName);
            }
            catch (Exception e) {
                var message = e.ToString();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be default.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardException_ToString)}()*");
                message.Should().Contain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void RewindCallStackMessage_GuardAgainstDefault() {
            var parameterName = "Jauser";

            try {
                Guard.Against.Default(new TimeSpan(), parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be default.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(RewindCallStackMessage_GuardAgainstDefault)}()*");
                message.Should().NotContain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void RewindCallStackMessage_GuardAgainstNull() {
            var parameterName = "Jauser";

            try {
                Guard.Against.Null<object>(null, parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be null.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(RewindCallStackMessage_GuardAgainstNull)}()*");
                message.Should().NotContain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }
    }
}