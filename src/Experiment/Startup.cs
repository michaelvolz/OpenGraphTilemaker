using System;
using System.Reflection;
using BlazorState;
using Common;
using Common.Blazor;
using Experiment.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        private static void VerifyCryptoWatchApiKey(ILogger<Startup> logger)
        {
            // var cryptoWatchOptions = ServiceLocator.Current.GetInstance<IOptions<CryptoWatchOptions>>();
            // if (cryptoWatchOptions == null || cryptoWatchOptions.Value.ApiKey == "n/a")
            // throw new InvalidOperationException("CryptoWatchOptions ApiKey not configured!");
            // logger.LogInformation("CryptoWatch ApiKey: " + cryptoWatchOptions.Value.ApiKey.TruncateAtWord(5, "..."));
        }

        private static ILogger<Startup> VerifyLogger()
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger<Startup>>();

            if (logger == null) throw new InvalidOperationException("ILogger<> not found!");

            return logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddBlazorState
            (
                aOptions => aOptions.Assemblies =
                    new[]
                    {
                        typeof(Startup).GetTypeInfo().Assembly
                    }
            );

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.Scan
            (
                aTypeSourceSelector => aTypeSourceSelector
                    .FromAssemblyOf<Startup>()
                    .AddClasses()
                    .AsSelf()
                    .WithScopedLifetime()
            );

            services.AddMemoryCache();

            services.AddTransient<Time>();

            services.AddHttpClient<ITileMakerClient, TileMakerClient>();

            services.AddTransient<OpenGraphTileMaker>();

            services.AddSingleton<WeatherForecastService>();

            services.AddTransient<Feed<PocketEntry>>();

            services.AddTransient<DiscCache>();
            services.Configure<DiscCacheOptions>(options =>
            {
                options.CacheState = CacheState.Enabled;
                options.CacheFolder = @"C:\WINDOWS\Temp\";
            });

            services.AddTransient<HttpLoader>();

            services.AddTransient<IPocket, Pocket>();
            services.Configure<PocketOptions>(options =>
            {
                options.Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
                options.CachingTimeSpan = TimeSpan.FromSeconds(15);
                options.TimeOutTimeSpan = TimeSpan.FromSeconds(10);
            });

            ServiceLocator.SetServiceProvider(services.BuildServiceProvider());

            var logger = VerifyLogger();
            VerifyCryptoWatchApiKey(logger);

            logger.LogWarning("Runtime has Mono: " + JsRuntimeLocation.HasMono);

            var jsRuntime = ServiceLocator.Current.GetInstance<IJSRuntime>() ??
                            throw new ArgumentNullException("ServiceLocator.Current.GetInstance<IJSRuntime>()");

            logger.LogWarning("jsRuntime found: " + jsRuntime);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}