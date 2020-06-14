using System;
using System.IO;
using Common;
using Domain.OpenGraphTilemaker.GetPocket;
using Domain.OpenGraphTilemaker.OpenGraph;
using Experiment.Features.App;
using Experiment.Features.CryptoWatch;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Experiment
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        [UsedImplicitly] public static IConfiguration? Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            AppServices(services);
        }

        [UsedImplicitly]
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AppServices(IServiceCollection services)
        {
            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Features");

            services.AddMemoryCache();

            Extensions.BlazorState(services);

            services.AddTransient<Time>();
            services.AddHttpClient<ITileMakerClient, TileMakerClient>();
            services.AddTransient<OpenGraphTileMaker>();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<Feed<PocketEntry>>();
            services.AddTransient<DiscCache>();
            services.Configure<DiscCacheOptions>(options =>
            {
                options.CacheState = CacheState.Enabled;
                options.CacheFolder = Path.GetTempPath();
            });

            services.AddTransient<HttpLoader>();
            services.AddTransient<IPocket, Pocket>();
            services.Configure<PocketOptions>(options =>
            {
                options.Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
                options.CachingTimeSpan = TimeSpan.FromSeconds(15);
                options.TimeOutTimeSpan = TimeSpan.FromSeconds(10);
            });

            services.Configure<CryptoWatchOptions>(Configuration?.GetSection("CryptoWatch"));

            ServiceLocator.SetServiceProvider(services.BuildServiceProvider());

            var logger = Extensions.VerifyLogger();
            Extensions.VerifyCryptoWatchApiKey(logger);
        }
    }
}
