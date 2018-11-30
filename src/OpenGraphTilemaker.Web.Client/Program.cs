using System;
using Microsoft.AspNetCore.Blazor.Hosting;
using OpenGraphTilemaker.Web.Client.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client
{
    public class Program
    {
        public static void Main(string[] args) {
            SelfLog.Enable(m => Console.Error.WriteLine(m));

            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.BrowserConsole()
                .WriteTo.BrowserHttp(controlLevelSwitch: levelSwitch)
                .CreateLogger();

            Log.Information("Hello, browser!");

            try {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "An exception occurred while creating the WASM host");
                throw;
            }
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseSerilog()
                .UseBlazorStartup<Startup>();
    }
}
