using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Tests
{
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