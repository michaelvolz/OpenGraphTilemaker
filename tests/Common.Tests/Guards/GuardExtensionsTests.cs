using System;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardNullOrWhiteSpace(string testParameter, string expected)
        {
            try
            {
                Guard.Against.NullOrWhiteSpace(() => testParameter);
            }
            catch (Exception e)
            {
                CheckExceptionResult(expected, e, nameof(testParameter), $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNullOrWhiteSpace)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardCondition(string parameterName)
        {
            try
            {
                Guard.Against.Condition(() => DateTime.Now != DateTime.MinValue, parameterName);
            }
            catch (Exception e)
            {
                CheckExceptionResult("Condition", e, parameterName, $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardCondition)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardAssert(string parameterName)
        {
            try
            {
                Guard.Against.Assert(() => DateTime.Now == DateTime.MinValue, parameterName);
            }
            catch (Exception e)
            {
                CheckExceptionResult("Assertion", e, parameterName, $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardAssert)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        private void CheckExceptionResult(string errorMessageFragment, Exception e, string parameterName, string methodNameWildCardPattern)
        {
            var message = e.RewindStackTraceMessage();
            message.Should().NotBeNullOrWhiteSpace();

            TestConsole.WriteLine(message);

            message.Should().StartWithEquivalent(GuardException.GuardPrefix);
            message.Should().Contain(parameterName);
            message.Should().Contain(errorMessageFragment);
            message.Should().MatchEquivalentOf(methodNameWildCardPattern);
            message.Should().NotContain("GuardClauseExtensions");
        }

        private enum TestEnum
        {
            Undefined = 0,
            TestOn = 1,
            TestOff = 2
        }

        [Theory]
        [AutoData]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardEnum(string parameterName)
        {
            var invalidValue = 3;
            try
            {
                Guard.Against.Enum(invalidValue, typeof(TestEnum), parameterName);
            }
            catch (Exception e)
            {
                CheckExceptionResult($"The value of argument '{parameterName}' ({invalidValue}) is invalid for Enum type '{nameof(TestEnum)}'.", e,
                    parameterName,
                    $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardEnum)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Theory]
        [AutoData]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardNullGeneric(string parameterName)
        {
            try
            {
                Guard.Against.Null<object>(null!, parameterName);
            }
            catch (Exception e)
            {
                CheckExceptionResult("Value cannot be null.", e, parameterName, $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNullGeneric)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        public void GuardDefault()
        {
            var parameter = default(TimeSpan);
            var parameterName = nameof(parameter);

            try
            {
                Guard.Against.Default(() => parameter);
            }
            catch (Exception e)
            {
                CheckExceptionResult("Value cannot be default.", e, parameterName, $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardDefault)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }

        [Fact]
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "Test")]
        public void GuardNull()
        {
            var parameter = (object?)null;
            var parameterName = nameof(parameter);

            try
            {
                Guard.Against.Null(parameter, nameof(parameter));
            }
            catch (Exception e)
            {
                CheckExceptionResult("Value cannot be null.", e, parameterName, $"*at*{nameof(GuardExtensionsTests)}.{nameof(GuardNull)}*");

                return;
            }

            "No exception thrown!".Should().Be("This should never execute!");
        }
    }
}
