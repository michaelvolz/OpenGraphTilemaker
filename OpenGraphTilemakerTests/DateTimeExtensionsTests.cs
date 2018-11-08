using System;
using System.Globalization;
using FluentAssertions;
using OpenGraphTilemaker;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemakerTests
{
    public class DateTimeExtensionsTests : BaseTest
    {
        public DateTimeExtensionsTests(ITestOutputHelper testConsole) : base(testConsole)
        {
        }

        private const string SingularPlural = "singular;plural";

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public static object[][] TestData => new[]
        {
            new object[] {DateTime.UtcNow, "just now"},
            new object[] {DateTime.UtcNow.AddMinutes(-1), "1 minute ago"},
            new object[] {DateTime.UtcNow.AddMinutes(-2), "2 minutes ago"},
            new object[] {DateTime.UtcNow.AddMinutes(-60), "1 hour ago"},
            new object[] {DateTime.UtcNow.AddMinutes(-120), "2 hours ago"},
            new object[] {DateTime.UtcNow.AddDays(-1), "yesterday"},
            new object[] {DateTime.UtcNow.AddDays(-2), "2 days ago"},
            new object[] {DateTime.UtcNow.AddDays(-7), "1 week ago"},
            new object[] {DateTime.UtcNow.AddDays(-30), "5 weeks ago"}
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ToFriendlyDate_ValidDates(DateTime date, string expected)
        {
            var result = date.ToFriendlyDate();

            result.Should().Be(expected);
        }

        [Fact]
        public void ToFriendlyDate_FutureDate_Invalid()
        {
            var result = DateTime.UtcNow.AddMinutes(5).ToFriendlyDate();

            result.Should().Be("n/a");
        }

        [Fact]
        public void ToFriendlyDate_OldDate()
        {
            var date = DateTime.UtcNow.AddMonths(-3);
            var result = date.ToFriendlyDate();

            result.Should().Be(date.ToLongDateString());
        }

        [Theory]
        [InlineData(0, "0 plural", SingularPlural)]
        [InlineData(1, "1 singular", SingularPlural)]
        [InlineData(2, "2 plural", SingularPlural)]
        [InlineData(-1, "-1 singular", SingularPlural)]
        [InlineData(-2, "-2 plural", SingularPlural)]
        [InlineData(0, "0 b", "a;b;c")]
        [InlineData(1, "1 a", "a;b;c")]
        [InlineData(7, "7 b", "a;b;c")]
        [InlineData(-1, "-1 a", "a;b;c")]
        [InlineData(-7, "-7 b", "a;b;c")]
        [InlineData(0, "0 just-one-text", "just-one-text")]
        [InlineData(1, "1 just-one-text", "just-one-text")]
        [InlineData(2, "2 just-one-text", "just-one-text")]
        [InlineData(-1, "-1 just-one-text", "just-one-text")]
        [InlineData(-2, "-2 just-one-text", "just-one-text")]
        [InlineData(12, "12", null)]
        public void PluralFormatProvider_ValidValues(int value, string expected, string format)
        {
            var sut = new DateTimeExtensions.PluralFormatProvider();

            var result = sut.Format(format, value, null);

            result.Should().Be(expected);
        }
    }
}