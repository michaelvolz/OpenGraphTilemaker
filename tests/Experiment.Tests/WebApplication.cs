using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Experiment.Tests
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class WebApplication : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected WebApplication(WebApplicationFactory<Startup> factory, ITestOutputHelper testConsole)
        {
            Factory = factory;
            TestConsole = testConsole;
        }

        protected WebApplicationFactory<Startup> Factory { get; }
        protected ITestOutputHelper TestConsole { get; }
    }
}