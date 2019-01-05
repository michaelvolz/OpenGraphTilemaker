﻿using System;
using System.Linq;
using Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

// ReSharper disable ClassNeverInstantiated.Global

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public class SeleniumServerFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private IWebHost _host;

        public SeleniumServerFactory() => ClientOptions.BaseAddress = new Uri("https://localhost");

        public ChromeOptions ChromeOptions {
            get {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
                return chromeOptions;
            }
        }

        public ChromeDriver Driver => new ChromeDriver(AssemblyExtensions.AssemblyLocation, ChromeOptions);

        public Uri RootUri { get; private set; }

        protected override Microsoft.AspNetCore.TestHost.TestServer CreateServer(IWebHostBuilder builder) {
            _host = builder.Build();
            _host.Start();

            RootUri = new Uri(_host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault());

            // Fake Server we won't use...this is lame. Should be cleaner, or a utility class
            return new Microsoft.AspNetCore.TestHost.TestServer(new WebHostBuilder().UseStartup<TStartup>());
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) _host?.Dispose();
        }
    }
}