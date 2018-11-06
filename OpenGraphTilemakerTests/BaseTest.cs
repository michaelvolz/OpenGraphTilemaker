using Xunit.Abstractions;

namespace OpenGraphTilemakerTests
{
    public class BaseTest
    {
        protected BaseTest(ITestOutputHelper testConsole)
        {
            TestConsole = testConsole;
        }

        protected ITestOutputHelper TestConsole { get; }
    }
}