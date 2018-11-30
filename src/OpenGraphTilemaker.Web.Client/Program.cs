using System;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.Web.Client.Diagnostics;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using ILogger = Serilog.ILogger;

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
                CreateHostBuilder(args)
                    //.UseSerilog()
                    .Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "An exception occurred while creating the WASM host");
                throw;
            }
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }

    public static class MyClass
    {
        public static IWebAssemblyHostBuilder UseSerilog(
            this IWebAssemblyHostBuilder builder, ILogger logger = null, bool dispose = false) {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            
            builder.ConfigureServices(col => col.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, dispose)));
            
            return builder;
        }
    }
}
