using System;
using BlazorState;
using Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using OpenGraphTilemaker.Web.Client.ClientApp.Services;

namespace OpenGraphTilemaker.Web.Client
{
    /// <summary>
    ///     Client Startup.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddBlazorState();

            services.AddMemoryCache();

            services.AddTransient<Time>();

            services.AddHttpClient<ITileMakerClient, TileMakerClient>();

            // BUG-FIX for 2.2 preview 3
            // .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

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
            });
        }

        [UsedImplicitly]
        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
    }
}