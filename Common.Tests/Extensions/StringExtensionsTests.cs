using BaseTestCode;
using Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Tests.Extensions
{
    public class StringExtensionsTests : BaseTest
    {
        public StringExtensionsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        private const string EmptyString = "";
        private const string WhiteSpaceString = " ";
        private const string StringDummy = "dummy";
        private const string Ellipsis = "…";

        private static readonly object EmptyObject = "";
        private static readonly object WhiteSpaceObject = " ";
        private static readonly object ObjectDummy = "dummy";

        public static object[][] TestData => new[] {
            new object[] {"Text1_Text2_Text3", 15, "Text1_Text2_Tex" + Ellipsis},
            new object[] {"Text1 Text2 Text3", 15, "Text1 Text2" + Ellipsis},
            new object[] {"Text1 - Text2 - Text3", 15, "Text1 - Text2 - " + Ellipsis},
            new object[] {"Text1 - Text2. Text3", 15, "Text1 - Text2. " + Ellipsis},
            new object[] {"Text1", 15, "Text1"}
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void TruncateAtWord_Scenario_Behavior(string text, int index, string expected) {
            var result = text.TruncateAtWord(index);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void IsNullOrEmpty_NullObject() {
            var result = ((object) null).IsNullOrEmpty();
            
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmpty_NullString() {
            var result = ((string) null).IsNullOrEmpty();
            
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmpty_ValidValues() {
            EmptyString.IsNullOrEmpty().Should().BeTrue();
            EmptyObject.IsNullOrEmpty().Should().BeTrue();
            WhiteSpaceString.IsNullOrEmpty().Should().BeFalse();
            WhiteSpaceObject.IsNullOrEmpty().Should().BeFalse();
            StringDummy.IsNullOrEmpty().Should().BeFalse();
            ObjectDummy.IsNullOrEmpty().Should().BeFalse();
        }

        [Fact]
        public void IsNullOrWhiteSpace_NullObject() {
            var result = ((object) null).IsNullOrWhiteSpace();
            
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrWhiteSpace_NullString() {
            var result = ((string) null).IsNullOrWhiteSpace();
            
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrWhiteSpace_ValidValues() {
            EmptyString.IsNullOrWhiteSpace().Should().BeTrue();
            EmptyObject.IsNullOrWhiteSpace().Should().BeTrue();
            WhiteSpaceString.IsNullOrWhiteSpace().Should().BeTrue();
            WhiteSpaceObject.IsNullOrWhiteSpace().Should().BeTrue();
            StringDummy.IsNullOrWhiteSpace().Should().BeFalse();
            ObjectDummy.IsNullOrWhiteSpace().Should().BeFalse();
        }

        [Fact]
        public void NotNullNorEmpty_NullObject() {
            var result = ((object) null).NotNullNorEmpty();
            
            result.Should().BeFalse();
        }

        [Fact]
        public void NotNullNorEmpty_NullString() {
            var result = ((string) null).NotNullNorEmpty();
            
            result.Should().BeFalse();
        }

        [Fact]
        public void NotNullNorEmpty_ValidValues() {
            EmptyString.NotNullNorEmpty().Should().BeFalse();
            EmptyObject.NotNullNorEmpty().Should().BeFalse();
            WhiteSpaceString.NotNullNorEmpty().Should().BeTrue();
            WhiteSpaceObject.NotNullNorEmpty().Should().BeTrue();
            StringDummy.NotNullNorEmpty().Should().BeTrue();
            ObjectDummy.NotNullNorEmpty().Should().BeTrue();
        }

        [Fact]
        public void NotNullNorWhiteSpace_NullObject() {
            var result = ((object) null).NotNullNorWhiteSpace();
            
            result.Should().BeFalse();
        }

        [Fact]
        public void NotNullNorWhiteSpace_NullString() {
            var result = ((string) null).NotNullNorWhiteSpace();
            
            result.Should().BeFalse();
        }

        [Fact]
        public void NotNullNorWhiteSpace_ValidValues() {
            EmptyString.NotNullNorWhiteSpace().Should().BeFalse();
            EmptyObject.NotNullNorWhiteSpace().Should().BeFalse();
            WhiteSpaceString.NotNullNorWhiteSpace().Should().BeFalse();
            WhiteSpaceObject.NotNullNorWhiteSpace().Should().BeFalse();
            StringDummy.NotNullNorWhiteSpace().Should().BeTrue();
            ObjectDummy.NotNullNorWhiteSpace().Should().BeTrue();
        }
    }
}