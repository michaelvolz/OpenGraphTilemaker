using Xunit.Abstractions;

// ReSharper disable CheckNamespace

public class BaseTest
{
    protected BaseTest(ITestOutputHelper testConsole) {
        TestConsole = testConsole;
    }

    protected ITestOutputHelper TestConsole { get; }
}