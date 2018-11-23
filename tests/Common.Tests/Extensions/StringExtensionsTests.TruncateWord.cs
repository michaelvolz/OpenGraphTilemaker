using Common.Extensions;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Tests.Extensions
{
    public partial class StringExtensionsTests
    {
        public static object[][] TestData { [UsedImplicitly] get; } = {
            new object[] { "Text1_Text2_Text3", 15, "Text1_Text2_Tex" + Ellipsis },
            new object[] { "Text1 Text2 Text3", 15, "Text1 Text2" + Ellipsis },
            new object[] { "Text1 - Text2 - Text3", 15, "Text1 - Text2" + Ellipsis },
            new object[] { "Text1 - Text2. Text3", 15, "Text1 - Text2" + Ellipsis },
            new object[] { "Text1", 15, "Text1" },
            new object[] { "Gunnar Peipman - Programming Blog", 25, "Gunnar Peipman" + Ellipsis },
            new object[] { "Gunnar Peipman - Programming Blog", 30, "Gunnar Peipman - Programming" + Ellipsis }
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void TruncateAtWord(string text, int index, string expected) {
            var result = text.TruncateAtWord(index);

            result.Should().BeEquivalentTo(expected);
            result.Trim().Length.Should().BeLessOrEqualTo(index + Ellipsis.Length);

        }
    }
}
