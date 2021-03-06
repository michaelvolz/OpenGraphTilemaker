﻿using System.Collections.Generic;
using Common.Extensions;
using FluentAssertions;
using Xunit;

namespace Common.Tests.Extensions
{
    public partial class StringExtensionsTests
    {
        public static IEnumerable<object[]> StringExtensionsTestData => new List<object[]>
        {
            new object[] { "Text1_Text2_Text3", 15, "Text1_Text2_Tex" + Ellipsis },
            new object[] { "Text1 Text2 Text3", 15, "Text1 Text2" + Ellipsis },
            new object[] { "Text1 - Text2 - Text3", 15, "Text1 - Text2" + Ellipsis },
            new object[] { "Text1 - Text2. Text3", 15, "Text1 - Text2" + Ellipsis },
            new object[] { "Text1", 15, "Text1" },
            new object[] { "Gunnar Peipman - Programming Blog", 25, "Gunnar Peipman" + Ellipsis },
            new object[] { "Gunnar Peipman - Programming Blog", 30, "Gunnar Peipman - Programming" + Ellipsis }
        };

        [Theory]
        [MemberData(nameof(StringExtensionsTestData))]
        public void TruncateAtWord(string text, int index, string expected)
        {
            var result = text.TruncateAtWord(index);

            result.Should().BeEquivalentTo(expected);
            result.Should().NotBeNullOrEmpty();
            result!.Trim().Length.Should().BeLessOrEqualTo(index + Ellipsis.Length);
        }
    }
}
