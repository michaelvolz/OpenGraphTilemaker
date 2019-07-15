using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace Experiment
{
    public class Program
    {
        private const string AppSettingsJSON = "appsettings.json";
        private const string FixedPathExtensionForTesting = @"..\..\..\..\..\..\src\OpenGraphTilemaker.Web.Server";

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(FindAppSettings())
            .AddJsonFile(AppSettingsJSON, false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .AddUserSecrets("15ea9d85-0125-491c-bbf6-6e4acc1703a6")
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithDemystifiedStackTraces()
                .Enrich.WithExceptionDetails()

                // .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}\t\t\t{Properties}{NewLine}\t\t\t{Exception}{NewLine}")
                .WriteTo.ColoredConsole(
                    outputTemplate:
                    "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
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

        private static string FindAppSettings()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            if (File.Exists(currentDirectory + @"\" + AppSettingsJSON)) return currentDirectory;

            return currentDirectory + FixedPathExtensionForTesting;
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