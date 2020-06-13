using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using BaseTestCode;
using Common.Extensions;
using FluentAssertions;
using VirtualTimeLib;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests.Extensions
{
    public class DateTimeExtensionsTests : BaseTest<DateTimeExtensionsTests>
    {
        private const string SingularPlural = "singular;plural";
        private const int FreezeTime = 0;

        public DateTimeExtensionsTests(ITestOutputHelper testConsole)
            : base(testConsole) { }

        public static IEnumerable<object[]> DateTimeExtensionsTestData => new List<object[]>
        {
            new object[] { DateTime.UtcNow.ToVirtualTime(FreezeTime), "just now" },
            new object[] { DateTime.UtcNow.AddMinutes(-1).ToVirtualTime(FreezeTime), "1 minute ago" },
            new object[] { DateTime.UtcNow.AddMinutes(-2).ToVirtualTime(FreezeTime), "2 minutes ago" },
            new object[] { DateTime.UtcNow.AddMinutes(-60).ToVirtualTime(FreezeTime), "1 hour ago" },
            new object[] { DateTime.UtcNow.AddMinutes(-120).ToVirtualTime(FreezeTime), "2 hours ago" },
            new object[] { DateTime.UtcNow.AddDays(-1).ToVirtualTime(FreezeTime), "yesterday" },
            new object[] { DateTime.UtcNow.AddDays(-2).ToVirtualTime(FreezeTime), "2 days ago" },
            new object[] { DateTime.UtcNow.AddDays(-7).ToVirtualTime(FreezeTime), "1 week ago" },
            new object[] { DateTime.UtcNow.AddDays(-30).ToVirtualTime(FreezeTime), "5 weeks ago" }
        };

        [Theory]
        [MemberData(nameof(DateTimeExtensionsTestData))]
        public void ToFriendlyDate_ValidDates(ITime datum, string expected)
        {
            var result = datum.Now.ToFriendlyDate();

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
        public void PluralFormatProvider_ValidValues(int value, string expected, string format, PluralFormatProvider sut)
        {
            var result = sut.Format(format, value, null);

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
    }
}
