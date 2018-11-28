using System;
using System.Linq;
using System.Net.Http;
using BlazorState;
using Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Blazor.Services;
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
            // Server Side Blazor doesn't register HttpClient by default
            if (services.All(x => x.ServiceType != typeof(HttpClient))) {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped(s => {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<IUriHelper>();
                    return new HttpClient {
                        BaseAddress = new Uri(uriHelper.GetBaseUri())
                    };
                });
            }

            services.AddBlazorState();

            services.AddMemoryCache();

            //services.AddTransient<Time>();

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
            });
        }

        [UsedImplicitly]
        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
