using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public class FunctionalTests : IClassFixture<WebApplicationFactory<Server.Startup>>
    {
        private readonly WebApplicationFactory<Server.Startup> _factory;

        public FunctionalTests(WebApplicationFactory<Server.Startup> factory, ITestOutputHelper testConsole) {
            _factory = factory;
            TestConsole = testConsole;
        }

        protected ITestOutputHelper TestConsole { get; }

        [Theory]
        [InlineData("http://localhost:50709")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url) {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(new Uri(url));

            // Assert
            response.Should().NotBeNull();
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            TestConsole.WriteLine(response.Content.Headers.ToString());

            var content = await response.Content.ReadAsStringAsync();
            TestConsole.WriteLine(content);

            content.Should().Contain("_framework/blazor.server.js");
        }
    }
}
