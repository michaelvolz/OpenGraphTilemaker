using System;
using System.Threading;
using BlazorState;
using Common;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using OpenGraphTilemakerWeb.App.ClientApp.Services;

namespace OpenGraphTilemakerWeb.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddBlazorState();

            services.AddMemoryCache();
            
            services.AddHttpClient<ITileMakerClient, TileMakerClient>()
                // BUG-FIX for 2.2 preview 3
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.AddTransient<OpenGraphTileMaker>();

            services.AddSingleton<WeatherForecastService>();
            
            services.AddTransient<FeedService<GetPocketEntry>>();

            services.AddTransient<DiscCache>();
            services.Configure<DiscCacheOptions>(options => { options.CacheFolder = @"C:\WINDOWS\Temp\"; });

            services.AddTransient<HttpLoader>();

            services.AddTransient<IGetPocket, GetPocket>();
            services.Configure<GetPocketOptions>(options => {
                options.Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
                options.CachingTimeSpan = TimeSpan.FromSeconds(15);
            });
        }

        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
    }
}