using System;
using System.Net.Http;
using Common.Extensions;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Server.Startup>>, IDisposable
    {
        public SeleniumTests(SeleniumServerFactory<Server.Startup> server) {
            Server = server;
            Client = server.CreateClient();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);

            var driver = new ChromeDriver(AssemblyExtensions.AssemblyLocation, chromeOptions);
            Browser = driver;
            Logs = new RemoteLogs(driver); //TODO: Still not bringing the logs over yet
        }

        public void Dispose() => Browser.Dispose();

        public SeleniumServerFactory<Server.Startup> Server { get; }
        public IWebDriver Browser { get; }
        public HttpClient Client { get; }
        public ILogs Logs { get; }

        [Fact]
        public void AppTitle_Found() {
            Browser.Navigate().GoToUrl(Server.RootUri);

            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(15));
            var tag = wait.Until(ExpectedConditions.ElementExists(By.ClassName("app-title")));

            tag.Text.Should().BeEquivalentTo("OpenGraph TileMaker");

            Logs.Should().NotBeNull();
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
