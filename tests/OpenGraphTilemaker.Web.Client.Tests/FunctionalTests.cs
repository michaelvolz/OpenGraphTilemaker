using System;
using System.Threading.Tasks;
using Common.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenGraphTilemaker.Web.Client.Tests.TestServer.Helpers;
using Xunit;
using Xunit.Abstractions;
using AngleSharp.Dom.Html;

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
        public async Task Get_EndpointReturnsSuccess(string url) {
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

            var doc = await HtmlHelpers.GetDocumentAsync(response);
            TestConsole.WriteLine(doc.ReturnDumpFlat());

            var element = doc.QuerySelector("#myApp");
            element.Should().NotBeNull();
            element.InnerHtml.Should().NotBeNullOrWhiteSpace();
            element.InnerHtml.Should().BeEquivalentTo("loading...");
        }
    }
}
