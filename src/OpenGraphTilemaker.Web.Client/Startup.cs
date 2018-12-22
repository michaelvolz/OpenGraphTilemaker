using System;
using System.Linq;
using System.Net.Http;
using BlazorState;
using Common;
using Common.Blazor;
using Common.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using OpenGraphTilemaker.Web.Client.ClientApp.Services;
using OpenGraphTilemaker.Web.Client.Features.CryptoWatch;

namespace OpenGraphTilemaker.Web.Client
{
    /// <summary>
    ///     Client Startup.
    /// </summary>
    public class Startup
    {
        private static void VerifyCryptoWatchApiKey(ILogger<Startup> logger) {
            var cryptoWatchOptions = ServiceLocator.Current.GetInstance<IOptions<CryptoWatchOptions>>();

            if (cryptoWatchOptions == null || cryptoWatchOptions.Value.ApiKey == "n/a")
                throw new InvalidOperationException("CryptoWatchOptions ApiKey not configured!");

            logger.LogInformation("CryptoWatch ApiKey: " + cryptoWatchOptions.Value.ApiKey.TruncateAtWord(5, "..."));
        }

        private static ILogger<Startup> VerifyLogger() {
            var logger = ServiceLocator.Current.GetInstance<ILogger<Startup>>();

            if (logger == null) throw new InvalidOperationException("ILogger<> not found!");

            return logger;
        }

        public void ConfigureServices(IServiceCollection services) {
            // Server Side Blazor doesn't register HttpClient by default
            if (services.All(x => x.ServiceType != typeof(HttpClient)))
                services.AddScoped(s => {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<IUriHelper>();
                    return new HttpClient {
                        BaseAddress = new Uri(uriHelper.GetBaseUri())
                    };
                });

            services.AddBlazorState();

            services.AddMemoryCache();

            services.AddTransient<Time>();

            services.AddHttpClient<ITileMakerClient, TileMakerClient>();

            services.AddTransient<OpenGraphTileMaker>();

            services.AddSingleton<WeatherForecastService>();

            services.AddTransient<Feed<PocketEntry>>();

            services.AddTransient<DiscCache>();
            services.Configure<DiscCacheOptions>(options => {
                options.CacheState = CacheState.Enabled;
                options.CacheFolder = @"C:\WINDOWS\Temp\";
            });

            services.AddTransient<HttpLoader>();

            services.AddTransient<IPocket, Pocket>();
            services.Configure<PocketOptions>(options => {
                options.Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
                options.CachingTimeSpan = TimeSpan.FromSeconds(15);
                options.TimeOutTimeSpan = TimeSpan.FromSeconds(10);
            });

            ServiceLocator.SetServiceProvider(services.BuildServiceProvider());

            var logger = VerifyLogger();
            VerifyCryptoWatchApiKey(logger);

            logger.LogWarning("Runtime has Mono: " + JsRuntimeLocation.HasMono);
        }

        [UsedImplicitly]
        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
