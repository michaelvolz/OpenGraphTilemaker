using System;
using System.Diagnostics.CodeAnalysis;
using Experiment.Features.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Experiment
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Program
    {
        public static int Main(string[] args)
        {
            Serilogger.Configure(ApplicationSettings.Configuration);

            try
            {
                Log.Information("Starting host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}