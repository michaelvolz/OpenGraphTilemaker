using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Experiment.Features.App
{
    public static class Serilogger
    {
        /// <summary>
        ///     https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(IConfiguration configuration) =>
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithDemystifiedStackTraces()
                .Enrich.WithExceptionDetails()
                // .WriteTo.ColoredConsole(
                //     outputTemplate: 
                //     "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}\t\t\t{Properties}{NewLine}\t\t\t{Exception}{NewLine}")
                .WriteTo.ColoredConsole(
                    outputTemplate:
                    "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                .WriteTo.Seq(
                    Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
                .CreateLogger();
    }
}