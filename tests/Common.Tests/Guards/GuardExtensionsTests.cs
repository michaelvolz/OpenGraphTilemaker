using System;
using Ardalis.GuardClauses;
using AutoFixture.Xunit2;
using BaseTestCode;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests.Guards
{
    public class GuardExtensionsTests : BaseTest<GuardExtensionsTests>
    {
        public GuardExtensionsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Theory]
        [InlineData(null, "Value cannot be null.")]
        [InlineData("", "Value cannot be empty.")]
        [InlineData("     ", "Value cannot be whitespace.")]
        public void GuardNullOrWhiteSpace(string testParameter, string expected) {
            try {
                Guard.Against.NullOrWhiteSpace(() => testParameter);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(nameof(testParameter));
                message.Should().Contain(expected);
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNullOrWhiteSpace)}*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        public void GuardCondition(string parameterName) {
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
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardCondition)}*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        public void GuardAssert(string parameterName) {
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
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardAssert)}*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        public enum TestEnum
        {
            Undefined = 0,
            TestOn = 1,
            TestOff = 2,
        }

        [Theory]
        [AutoData]
        public void GuardEnum(string parameterName) {
            var invalidValue = 3;
            try {
                Guard.Against.Enum(invalidValue, typeof(TestEnum), parameterName);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain($"The value of argument '{parameterName}' ({invalidValue}) is invalid for Enum type '{nameof(TestEnum)}'.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardEnum)}*");
                message.Should().NotContain("GuardClauseExtensions");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        public void RewindCallStackMessage_GuardAgainstNull(string parameterName) {
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
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(RewindCallStackMessage_GuardAgainstNull)}*");
                message.Should().NotContain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void GuardException_ToString() {
            var parameter = default(TimeSpan);
            var parameterName = nameof(parameter);

            try {
                Guard.Against.Default(() => parameter);
            }
            catch (Exception e) {
                var message = e.ToString();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be default.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardException_ToString)}*");
                message.Should().Contain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void GuardNull() {
            var parameter = (object)null;
            var parameterName = nameof(parameter);

            try {
                Guard.Against.Null(() => parameter);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be null.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNull)}*");
                message.Should().NotContain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        public void RewindCallStackMessage_GuardAgainstDefault() {
            var parameter = default(TimeSpan);
            var parameterName = nameof(parameter);

            try {
                Guard.Against.Default(() => parameter);
            }
            catch (Exception e) {
                var message = e.RewindStackTraceMessage();
                message.Should().NotBeNullOrWhiteSpace();

                TestConsole.WriteLine(message);

                message.Should().StartWithEquivalent(GuardException.GuardPrefix);
                message.Should().Contain(parameterName);
                message.Should().Contain("Value cannot be default.");
                message.Should().MatchEquivalentOf($"*at*{nameof(GuardExtensionsTests)}.{nameof(RewindCallStackMessage_GuardAgainstDefault)}*");
                message.Should().NotContain("GuardClauseExtensions");
                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }
    }
}
