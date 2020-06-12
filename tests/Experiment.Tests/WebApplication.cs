using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

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

        [UsedImplicitly]
        protected ITestOutputHelper TestConsole { get; }
    }
}
