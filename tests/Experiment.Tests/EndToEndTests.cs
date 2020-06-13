using System;
using System.Threading.Tasks;
using Experiment.Tests.TestServer.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Experiment.Tests
{
    public class EndToEndTests : WebApplication
    {
        public EndToEndTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testConsole)
            : base(factory, testConsole) { }

        [Theory]
        [InlineData("http://localhost:50709/counter")]
        public async Task CounterPage_SuccessStatusCode(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(new Uri(url));

            // Assert
            response.Should().NotBeNull();
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var html = await response.Content.ReadAsStringAsync();
            html.Should().Contain("app id=\"myApp\"");

            var doc = await response.ToHtmlDocumentAsync();
            var element = doc.QuerySelector("#myApp");
            element.Should().NotBeNull();
            element.InnerHtml.Should().NotBeNullOrWhiteSpace();
            element.InnerHtml.Should().Contain("Current count: 3");
        }
    }
}
