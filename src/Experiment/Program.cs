using System;
using System.Diagnostics.CodeAnalysis;
using Experiment.Features.AppCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Experiment
{
    public sealed partial class Program
    {








        //aaaaaaaaaaaa
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By Design")]
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

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
