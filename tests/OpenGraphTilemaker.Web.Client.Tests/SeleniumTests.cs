﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Common.Extensions;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public sealed class SeleniumTests : IClassFixture<SeleniumServerFactory<Server.Startup>>, IDisposable
    {
        public SeleniumTests(SeleniumServerFactory<Server.Startup> server, ITestOutputHelper testConsole) {
            _testConsole = testConsole;

            Server = server;
            Client = server.CreateClient();
            Browser = server.Driver;
            Logs = new RemoteLogs((RemoteWebDriver)Browser);
        }

        public void Dispose() => Browser.Dispose();

        private readonly ITestOutputHelper _testConsole;

        public SeleniumServerFactory<Server.Startup> Server { get; }
        public IWebDriver Browser { get; }
        public HttpClient Client { get; }
        public ILogs Logs { get; }

        private void WriteLogs() {
            Logs.Should().NotBeNull();
            var result = Logs.GetLog(LogType.Browser).Aggregate(string.Empty, (current, next) => current += next + Environment.NewLine);
            _testConsole.WriteLine(result);
        }

        [Fact]
        public void Blazor_BrowserTitle_Correct() {
            Browser.Navigate().GoToUrl(Server.RootUri);

            Browser.Title.Should().BeEquivalentTo("OpenGraphTilemaker Blazor ServerSide Sample App");

            WriteLogs();
        }

        [Fact]
        public void Blazor_AppTitle_Found() {
            Browser.Navigate().GoToUrl(Server.RootUri);

            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(60));
            var tag = wait.Until(ExpectedConditions.ElementExists(By.ClassName("app-title")));

            tag.Text.Should().BeEquivalentTo("OpenGraph TileMaker");

            WriteLogs();
        }

        [Fact(Skip = "Example")]
        public void TestWithChromeDriver() {
            using (var driver = new ChromeDriver(AssemblyExtensions.AssemblyLocation)) {
                driver.Navigate().GoToUrl(new Uri(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/"));

                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
            }
        }
    }
}