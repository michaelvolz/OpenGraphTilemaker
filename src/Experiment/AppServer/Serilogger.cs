using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace Experiment.AppServer
{
    public class Serilogger
    {
        public static void Configure(IConfiguration configuration) =>
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithDemystifiedStackTraces()
                .Enrich.WithExceptionDetails()

                // .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}\t\t\t{Properties}{NewLine}\t\t\t{Exception}{NewLine}")
                .WriteTo.ColoredConsole(
                    outputTemplate:
                    "{Timestamp:yy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                .CreateLogger();
    }
}