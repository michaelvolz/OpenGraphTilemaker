using System;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.AspNetCore;
using SerilogILogger = Serilog.ILogger;

namespace OpenGraphTilemaker.Web.Client.ClientApp.Diagnostics
{
    public static class SerilogExtensions
    {
        public static IWebAssemblyHostBuilder UseSerilog(this IWebAssemblyHostBuilder builder, SerilogILogger logger = null, bool dispose = false) {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(
                services => {
                    services.AddSingleton(provider => (ILoggerFactory)new SerilogLoggerFactory(logger, dispose));
                    services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
                });

            return builder;
        }
    }
}
