using System;
using BlazorState;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenGraphTilemaker;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using OpenGraphTilemakerWeb.App.ClientApp.Services;

namespace OpenGraphTilemakerWeb.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddBlazorState();

            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<IGetPocket, GetPocket>();
            services.AddTransient<OpenGraphTileMaker>();
            services.AddTransient<FeedService<GetPocketEntry>>();
            services.Configure<GetPocketOptions>(options => {
                options.Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
                options.CachingTimeSpan = TimeSpan.FromSeconds(15);
            });

            services.Configure<OpenGraphTileMakerOptions>(options => {
                options.CacheFolder = @"C:\WINDOWS\Temp\";
            });
            
        }

        // ReSharper disable All
        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
        // ReSharper restore All
    }
}