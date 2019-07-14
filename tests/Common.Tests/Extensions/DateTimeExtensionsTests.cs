using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using BaseTestCode;
using Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Tests.Extensions
{
    public class DateTimeExtensionsTests : BaseTest<DateTimeExtensionsTests>
    {
        public DateTimeExtensionsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        private const string SingularPlural = "singular;plural";

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { Now(), "just now" },
                new object[] { Now().AddMinutes(-1), "1 minute ago" },
                new object[] { Now().AddMinutes(-2), "2 minutes ago" },
                new object[] { Now().AddMinutes(-60), "1 hour ago" },
                new object[] { Now().AddMinutes(-120), "2 hours ago" },
                new object[] { Now().AddDays(-1), "yesterday" },
                new object[] { Now().AddDays(-2), "2 days ago" },
                new object[] { Now().AddDays(-7), "1 week ago" },
                new object[] { Now().AddDays(-30), "5 weeks ago" }
            };

        private static DateTime Now() => DateTime.UtcNow;

        [Theory]
        [MemberData(nameof(TestData))]
        public void ToFriendlyDate_ValidDates(DateTime date, string expected) {
            var result = date.ToFriendlyDate();

            result.Should().Be(expected);
        }

        [Theory]
        [InlineAutoData(0, "0 plural", SingularPlural)]
        [InlineAutoData(1, "1 singular", SingularPlural)]
        [InlineAutoData(2, "2 plural", SingularPlural)]
        [InlineAutoData(-1, "-1 singular", SingularPlural)]
        [InlineAutoData(-2, "-2 plural", SingularPlural)]
        [InlineAutoData(0, "0 b", "a;b;c")]
        [InlineAutoData(1, "1 a", "a;b;c")]
        [InlineAutoData(7, "7 b", "a;b;c")]
        [InlineAutoData(-1, "-1 a", "a;b;c")]
        [InlineAutoData(-7, "-7 b", "a;b;c")]
        [InlineAutoData(0, "0 just-one-text", "just-one-text")]
        [InlineAutoData(1, "1 just-one-text", "just-one-text")]
        [InlineAutoData(2, "2 just-one-text", "just-one-text")]
        [InlineAutoData(-1, "-1 just-one-text", "just-one-text")]
        [InlineAutoData(-2, "-2 just-one-text", "just-one-text")]
        [InlineAutoData(12, "12", null)]
        public void PluralFormatProvider_ValidValues(int value, string expected, string format, PluralFormatProvider sut) {
            var result = sut.Format(format, value, null);

            result.Should().Be(expected);
        }

        [Fact]
        public void ToFriendlyDate_FutureDate_Invalid() {
            var result = DateTime.UtcNow.AddMinutes(5).ToFriendlyDate();

            result.Should().Be("n/a");
        }

        [Fact]
        public void ToFriendlyDate_OldDate() {
            var date = DateTime.UtcNow.AddMonths(-3);
            var result = date.ToFriendlyDate();

            result.Should().Be(date.ToLongDateString());
        }
    }
}
