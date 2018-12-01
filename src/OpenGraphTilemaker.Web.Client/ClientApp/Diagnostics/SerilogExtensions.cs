using System;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.AspNetCore;
using ILogger = Serilog.ILogger;

namespace OpenGraphTilemaker.Web.Client.ClientApp.Diagnostics
{
    public static class SerilogExtensions
    {
        public static IWebAssemblyHostBuilder UseSerilog(this IWebAssemblyHostBuilder builder, ILogger logger = null, bool dispose = false) {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(
                serviceCollection => serviceCollection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, dispose)));

            return builder;
        }
    }
}
