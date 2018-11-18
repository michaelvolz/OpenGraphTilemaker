using Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Tests.Extensions
{
    public partial class StringExtensionsTests
    {
        public static object[][] TestData => new[] {
            new object[] {"Text1_Text2_Text3", 15, "Text1_Text2_Tex" + Ellipsis},
            new object[] {"Text1 Text2 Text3", 15, "Text1 Text2" + Ellipsis},
            new object[] {"Text1 - Text2 - Text3", 15, "Text1 - Text2 - " + Ellipsis},
            new object[] {"Text1 - Text2. Text3", 15, "Text1 - Text2. " + Ellipsis},
            new object[] {"Text1", 15, "Text1"}
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public void TruncateAtWord(string text, int index, string expected) {
            var result = text.TruncateAtWord(index);

            result.Should().BeEquivalentTo(expected);
        }
    }
}